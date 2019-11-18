from telegram.ext import Updater
from telegram.ext import MessageHandler, CommandHandler, Filters
import logging

logger = logging.getLogger('bot_logger')
logger.setLevel(logging.DEBUG)
ch = logging.FileHandler('logs/bot.log')
ch.setLevel(logging.DEBUG)
formatter = logging.Formatter(
    '%(asctime)s - %(name)s - %(levelname)s - %(message)s')
ch.setFormatter(formatter)
logger.addHandler(ch)

logger.debug("asd")

class TelegramBot:

    def __init__(self, token):
        self.__updater = Updater(token=token, use_context=True)
        self.__dispatcher = self.__updater.dispatcher
        self.__echo_handler = CommandHandler('start', self.__start_message)
        self.__dispatcher.add_handler(self.__echo_handler)

        logger.info(u"Bot initialized. TOKEN: " + token)

    def start(self):
        self.__updater.start_polling()
        logger.info(u"Bot started")

    def __start_message(self, update, context):
        context.bot.send_message(
            chat_id=update.effective_chat.id, text="")
