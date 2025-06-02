import threading
from datetime import datetime, timedelta
import socket
import time
import sys

from message import *

@dataclass
class User:
    id: int

    def name(self) -> str:
        if self.id == 10:
            return f"   Главный поток   10"
        elif self.id == 50:
            return f"   Все потоки      50"
        else:
            return f"   Пользователь    {self.id}"

class PythonClient:
    def __init__(self, host: str, port: int):
        self.HOST = host
        self.PORT = port
        self.running: bool = None
        self.other_ids: list[int] = [10, 50]
        self.history: list[Message] = []

        self._socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.connect()

    def get_valid_input(self, valid_list: list):
        while True:
            user_input = input()
            try:
                num = int(user_input)
                if num in valid_list:
                    return num
                else:
                    print("Вариант не корректный. Попробуйте заново...")
            except ValueError:
                print("Ошибка: введите целое число!")

    def menu(self):
        print(f"Ваш ClientID", self.my_id)
        print("1. Список пользователей")
        print("2. История сообщений")
        print("3. Отправить сообщение")
        print("0. Выход")
    
        print("Выберите действие: ")
        valid_cases = (0, 1, 2, 3)
        number = self.get_valid_input(valid_cases)
        match number:
            case 1:
                self.view_others_users()
            case 2:
                self.view_message_history()
            case 3: 
                self.send_message()
            case 0:
                sys.exit()

    def send_message(self):
        try:
            print("Выберите Id пользователя: ")
            to = self.get_valid_input(self.other_ids)
            message = input("Введите сообщение...")
            m = Message(to, self.my_id, MessageType.MT_DATA, message)
            self.history.append(m)
            m.Send(self._socket)
        
        except:
            print("Сообщение не отправлено")

    
    def view_others_users(self):
        print("Список пользователей:")
        for id in self.other_ids:
            user = User(id)
            print(user.name())
        print("Нажмите Enter для продолжения...")
        input()

    def view_message_history(self):
        for message in self.history:
            msg_prefix = f"{message.Header.From} → {message.Header.To}: "
            full_msg = msg_prefix + message.Data
            print(full_msg)
        print("Нажмите Enter для продолжения...")
        input()

    def process_messages(self):
        while self.running:
            m = Message.SendMessage(
                self._socket, MessageRecipient.MR_BROKER, MessageType.MT_GETDATA
            )
            if m.Header.Type == MessageType.MT_DATA:
                self.history.append(m)
            elif m.Header.Type == MessageType.MT_INIT:
                self.other_ids.append(m.Header.From)
            else:
                time.sleep(1)

    def connect(self):
        self._socket.connect((self.HOST, self.PORT))

        m = Message.SendMessage(
            self._socket, MessageRecipient.MR_BROKER, MessageType.MT_INIT
        )
        if m.Header.Type != MessageType.MT_INIT:
            raise Exception()

        self.my_id = Message.ClientID

        self.running = True
        t = threading.Thread(target=self.process_messages)
        t.start()

        while True:
            self.menu()


if __name__ == "__main__":
    HOST = 'localhost'
    PORT = 12345

    app = PythonClient(HOST, PORT)