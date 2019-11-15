from app_settings import AppSettings
from bot import TelegramBot

cfg = AppSettings(configs_path='config')

bot = TelegramBot()
bot.start()

