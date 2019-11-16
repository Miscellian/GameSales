from app_settings import AppSettings
from bot import TelegramBot
import os

# CONFIG_PATH - env var for your config path
cfg = AppSettings(configs_path=os.environ[("CONFIG_PATH")])

bot = TelegramBot(os.environ[cfg.token])
bot.start()

