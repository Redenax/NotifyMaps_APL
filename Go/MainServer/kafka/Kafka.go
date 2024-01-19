package kafka

import (
	config "MainServer/config"
	"context"
	"fmt"
	"log"
	"os"

	_ "github.com/go-sql-driver/mysql"
	"github.com/segmentio/kafka-go"
	"github.com/sony/gobreaker"
)

var breaker *gobreaker.CircuitBreaker

func init() {
	log.SetOutput(os.Stdout)
	// Inizializza il circuit breaker
	breaker = config.GetBreakerSettings("BreakerConnectionKafka")
}

func executeConnectionBreaker(breaker *gobreaker.CircuitBreaker, brokerAddress string) (*kafka.Conn, error) {
	var admin *kafka.Conn
	var err error
	// Use gobreaker.Execute to handle circuit breaking
	_, execErr := breaker.Execute(func() (interface{}, error) {
		// Inside the Execute function, create a new Kafka connection
		admin, err = kafka.DialContext(context.Background(), "tcp", brokerAddress)
		if err != nil {
			log.Printf("Error connecting to Kafka: %v", err)
			return nil, err
		}

		return admin, nil
	})

	if execErr != nil {
		// If the circuit is open, execErr will be gobreaker.ErrOpenState
		return nil, execErr
	}

	return admin, nil
}
func GetKafkaAddress() string {

	brokerAddress, err := config.GetParametroFromConfig("kafkabrokeraddress")

	if err != nil {
		log.Println(err)
		return ""
	}

	return brokerAddress
}

func KafkaStartup(province []string) {
	brokerAddress := GetKafkaAddress()

	var err error
	var a, b, c string
	var inserimento bool

	admin, err := executeConnectionBreaker(breaker, brokerAddress)
	for breaker.State().String() == gobreaker.StateClosed.String() && err != nil {
		admin, err = executeConnectionBreaker(breaker, brokerAddress)
	}

	if breaker.State().String() != gobreaker.StateClosed.String() {

		log.Println("Stato Aperto " + breaker.Name())
		a = "Errore Connessione"
		log.Printf("Error %s : %s ", err, a)
		return
	}

	if admin != nil {
		defer admin.Close()
	}

	if err != nil {
		log.Fatalf("Errore : %v", err)
	}
	// Ottieni la lista dei topic
	topics, err := admin.ReadPartitions()
	if err != nil {
		log.Fatalf("Errore ottenimento lista topic: %v", err)
	}
	if topics == nil {
		log.Fatalf("Errore ottenimento lista topic: %v", err)
	}
	log.Println("Lista dei topic:")
	for i := range province {

		for j := range province {

			if province[i] != province[j] {
				a = string(province[i])
				b = string(province[j])

				// Specifica il nome del topic da creare
				topic := fmt.Sprintf("%s_%s", a, b)

				// Stampa la lista dei topic

				for _, topiz := range topics {

					c = fmt.Sprintf(topiz.Topic)
					if topic == c {
						log.Println("Topic:", c, "Non inserito, gia presente")
						inserimento = true
						break
					} else {

						inserimento = false
					}
				}

				if !inserimento {
					// Crea il topic
					err = admin.CreateTopics(kafka.TopicConfig{
						Topic:             topic,
						NumPartitions:     1, // Numero di partizioni del topic
						ReplicationFactor: 1, // Fattore di replicazione del topic
					})
					if err != nil {
						log.Fatalf("Errore creazione topic Kafka: %v", err)
					}
					fmt.Printf("\n Il topic %s è stato creato con successo.\n", topic)
				}
				inserimento = true
			}
		}
	}

}

func KafkaProducer(topic string, messaggio string) {
	brokerAddress := GetKafkaAddress()

	admin, err := executeConnectionBreaker(breaker, brokerAddress)

	for breaker.State().String() == gobreaker.StateClosed.String() && err != nil {

		admin, err = executeConnectionBreaker(breaker, brokerAddress)
	}

	if breaker.State().String() != gobreaker.StateClosed.String() {

		log.Println("Stato Aperto " + breaker.Name())

		log.Printf("Error %s ", err)
		return
	}

	if admin != nil {
		defer admin.Close()
	}
	if err != nil {
		log.Printf("Error %s ", err)
		return
	}

	// Create a new Kafka writer
	writer := kafka.NewWriter(kafka.WriterConfig{
		Brokers:  []string{brokerAddress},
		Topic:    topic,
		Balancer: &kafka.LeastBytes{},
	})

	// Produce a message to the topic
	message := kafka.Message{
		Value: []byte(messaggio),
	}

	err = writer.WriteMessages(context.Background(), message)
	if err != nil {
		log.Printf("Errore nella scrittura : %v\n", err)
		return
	}

	log.Println("Messaggio inviato al topic: " + topic)

	// Close the writer
	err = writer.Close()
	if err != nil {
		log.Printf("Errore chiusura Kafka writer: %v\n", err)
		return
	}

}
