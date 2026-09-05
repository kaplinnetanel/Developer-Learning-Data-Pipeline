from confluent_kafka import Consumer
from process_record import process_record
from producer import send_processed, flush
import json
import os

def consumer():
    conf = {
        "bootstrap.servers": os.getenv(
            "KAFKA_BOOTSTRAP_SERVERS",
            "localhost:9092"
        ),
        "group.id": "foo",
        "auto.offset.reset": "smallest"
    }

    c = Consumer(conf)

    try:
        c.subscribe(["raw-survey-topic"])
        b = 0 
        while True:
            msg = c.poll(1.0)

            if msg is None:
                continue

            if msg.error():
                print("Eror:",msg.value())
                continue
            raw_record = json.loads(msg.value().decode("utf-8"))
            processed_record = process_record(raw_record)

            send_processed(processed_record)
            if b == 500:
                flush()

            print("Received:", msg.value())
    except KeyboardInterrupt:
        print("Stopping consumer...")



    finally:
        c.close()


consumer()        