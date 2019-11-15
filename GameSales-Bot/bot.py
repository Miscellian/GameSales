from telegram.ext import Updater
from telegram.ext import MessageHandler, CommandHandler, Filters
from app_settings import AppSettings
import os

cfg = AppSettings(configs_path='config')

class TelegramBot:
    def __init__(self):
        self.__updater = Updater(token=os.environ[cfg.token], use_context=True)
        self.__dispatcher = self.__updater.dispatcher

    def start(self):
        self.__updater.start_polling()
        self.__echo_handler = CommandHandler('start', self.__start_message)
        self.__dispatcher.add_handler(self.__echo_handler)

    def __start_message(self, update, context):
        context.bot.send_message(chat_id=update.effective_chat.id, text=cfg.start_message)
