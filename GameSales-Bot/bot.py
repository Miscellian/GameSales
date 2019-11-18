from telegram.ext import Updater
from telegram.ext import MessageHandler, CommandHandler, Filters
import logging
import logging.config
from logging import DEBUG, INFO, WARN, ERROR, CRITICAL
import yaml

with open("config/config.yml", 'r') as ymlfile:
    cfg = yaml.load(ymlfile, Loader=yaml.FullLoader)

with open("config/logging.yml", 'r') as logfile:
    config = yaml.safe_load(logfile.read())
logging.config.dictConfig(config)

logger = logging.getLogger('bot_logger')

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
            chat_id=update.effective_chat.id, text=cfg['bot']['start_message'])

    def stop(self):
        self.__updater.idle()
        logger.info(u"Bot stopped")
