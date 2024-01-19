import asyncio
import threading
import telegram
from confluent_kafka import Consumer, KafkaError

# Variabile di controllo per il consumatore periodico
keep_running = False


def handle_kafka_message(message, chat_id, api_bot):
    global keep_running
    # Gestisci i messaggi da Kafka qui solo se keep running è true
    if keep_running:
        print(f"Received message: {message.value().decode('utf-8')}")
        tg_msg = message.value().decode('utf-8')
        bot = telegram.Bot(api_bot)

        # Invia il messaggio Telegram utilizzando l'API del bot
        asyncio.run(bot.send_message(chat_id, text=tg_msg))


def set_kafka_consumer(kafka_config, topics, chat_id, api_bot):
    # Crea un consumatore Kafka con le configurazioni specificate
    consumer = Consumer(kafka_config)
    print(topics)
    consumer.subscribe(topics)

    try:
        while keep_running:
            # Polling per ricevere messaggi da Kafka
            msg = consumer.poll(10)
            print(msg)
            if msg is None:
                continue
            if msg.error():
                # Gestione degli errori Kafka
                if msg.error().code() == KafkaError._PARTITION_EOF:
                    continue
                else:
                    print(f"Errore Kafka: {msg.error()}")
                    break

            # Chiamata alla funzione per gestire il messaggio Kafka
            handle_kafka_message(msg, chat_id, api_bot)

    except KeyboardInterrupt:
        pass
    finally:
        print("annullo iscrizione al topic")
        # Annulla l'iscrizione e chiudi il consumatore Kafka
        consumer.unsubscribe()
        consumer.close()


class KafkaConsumer:

    def __init__(self, broker, chat_id, topics, api_bot):
        self.broker = broker
        self.chat_id = chat_id
        self.topics = topics
        self.api_bot = api_bot

    def periodic_kafka_consumer(self):
        global keep_running
        # Configurazione di Kafka
        kafka_config = {
            'bootstrap.servers': self.broker,
            'group.id': self.chat_id,
            'auto.offset.reset': 'earliest'
        }
        # Esegui il consumatore Kafka periodicamente ogni volta che keep_running è True
        while keep_running:
            set_kafka_consumer(kafka_config, self.topics, self.chat_id, self.api_bot)

    @staticmethod
    def logout_command():
        global keep_running
        # Chiamato quando viene eseguito il comando di logout
        print("Logout command executed.")
        keep_running = False

    def start_consumer(self):
        global keep_running
        keep_running = True
        # Avvia il thread per il consumatore Kafka periodico
        kafka_thread = threading.Thread(target=self.periodic_kafka_consumer)
        kafka_thread.start()
