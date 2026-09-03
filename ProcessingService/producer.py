from confluent_kafka import Producer
import json
producer = Producer({"bootstrap.servers":"localhost:9092"})
TopiC = "processed-survey-topic"

def  send_processed(record):
    producer.produce(TopiC,value=json.dumps(record, ensure_ascii=False).encode("utf-8")
    )

def flush():
    producer.flush()
