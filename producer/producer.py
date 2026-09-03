from confluent_kafka import Producer
import socket
import csv
import json

producer = Producer({'bootstrap.servers': 'localhost:9092'})

topic = "raw-survey-topic"

with open ("developer_ai_learning_raw.csv",encoding='utf-8') as f:
    for row in csv.DictReader(f):
        producer.produce(topic,value=json.dumps(row))
        print(row)
producer.flush() 