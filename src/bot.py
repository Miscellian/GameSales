from telegram.ext import Updater
from telegram.ext import MessageHandler, CommandHandler, Filters
from logging import DEBUG, INFO, WARN, ERROR, CRITICAL
import logging
import telegram
import logging.config
import yaml
from steam_api import SteamAPI

with open("config/config.yml", 'r') as ymlfile:
    cfg = yaml.load(ymlfile, Loader=yaml.FullLoader)

with open("config/logging.yml", 'r') as logfile:
    config = yaml.safe_load(logfile.read())
logging.config.dictConfig(config)

logger = logging.getLogger('bot_logger')

class TelegramBot:

    def __init__(self, token):
        self.__steam_api = SteamAPI()
        self.__updater = Updater(token=token, use_context=True)
        self.__dispatcher = self.__updater.dispatcher
        self.__start_handler = CommandHandler("start", self.__start_message)
        self.__get_sale_handler = CommandHandler("getSale", self.__get_sale_message)
        self.__all_message_handler = MessageHandler(Filters.all, self.__all_message)
        self.__dispatcher.add_handler(self.__start_handler)
        self.__dispatcher.add_handler(self.__get_sale_handler)
        self.__dispatcher.add_handler(self.__all_message_handler)
        logger.info(u"Bot initialized. TOKEN: " + token)

    def start(self):
        self.__updater.start_polling()
        logger.info(u"Bot started")

    def __all_message(self, update, context):
        user = update.message.from_user
        message = update.message.text
        logger.info("Text: %s. From: %s %s, @%s, %d" % (message, user.first_name, user.last_name, user.username, user.id))

    def __start_message(self, update, context):
        user = update.message.from_user
        message = update.message.text
        logger.info("Text: %s. From: %s %s, @%s, %d" % (message, user.first_name, user.last_name, user.username, user.id))
        context.bot.send_message(
            chat_id=update.effective_chat.id, text=cfg['bot']['start_message'])

    def __get_sale_message(self, update, context):
        user = update.message.from_user
        message = update.message.text
        logger.info("Text: %s. From: %s %s, @%s, %d" % (message, user.first_name, user.last_name, user.username, user.id))
        if (len(context.args) == 0):
            context.bot.send_message(chat_id=update.effective_chat.id, text="Invalid command syntax. Try: /getSale gameName")
        else:
            game_name = " ".join(context.args)
            price_overview = self.__steam_api.getPriceOverviewForGame(game_name.strip())
            if (len(price_overview) == 0):
                context.bot.send_message(chat_id=update.effective_chat.id, text="Game with this name doesn't exists in Steam")
            else: 
                currency = price_overview['currency']
                initial = price_overview['initial']
                final = price_overview['final']
                discount = price_overview['discount_percent']
                price_message = "<b>Now</b>: %d%s\n" % (final // 100, currency)
                if (discount != 0):
                    price_message += "<b>Discount</b>: %d%%\n" % (discount)
                    price_message += "<b>Initial</b>: %d%s" % (initial // 100, currency) 
                context.bot.send_message(chat_id=update.effective_chat.id, text=price_message, parse_mode=telegram.ParseMode.HTML)

    def stop(self):
        self.__updater.idle()
        logger.info(u"Bot stopped")
        
