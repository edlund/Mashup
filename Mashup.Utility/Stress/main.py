import json
import random
import sys
import threading
import time

import requests

from typing import Any, Callable, Dict, List

"""
A tiny utilty script for stress testing Mashup.
"""

print_lock = threading.Lock()

def safe_print(*args, **kwargs) -> None:
    print_lock.acquire()
    print(*args, **kwargs)
    print_lock.release()

def worker(artists: Dict[str, str], name: str, stop: Callable[[], bool]) -> None:
    session = requests.Session()
    safe_print("[{}] Up and running".format(name))
    while not stop():
        artist_mbid, artist_name = random.choice(list(artists.items()))
        safe_print("[{}] Requesting a summary about: {}".format(name, artist_name))
        response = session.get("http://localhost:5000/api/v1/artistsummary/getone?mbId={}".format(artist_mbid))
        safe_print("[{}] Received: {}".format(name, response))

def main(args: List[str]) -> None:
    artists: Dict[str, str] = {}
    with open("artists.json", 'r') as f:
        artists = json.loads(f.read())
    stop = False
    workers = 8
    threads = [
        threading.Thread(target=worker, kwargs={
            "artists": artists,
            "name": "Thread #{:08d}".format(i + 1),
            "stop": lambda: stop
        })
        for i in range(workers)
    ]
    for thread in threads:
        thread.start()
    for _ in range(16):
        time.sleep(1.0)
    stop = True
    for thread in threads:
        thread.join()

if __name__ == "__main__":
    main(sys.argv)
