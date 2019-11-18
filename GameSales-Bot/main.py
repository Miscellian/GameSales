import app_settings
from bot import TelegramBot
import logging

# export CONFIG_ENV=config
cfg = app_settings.AppSettings(env_name='CONFIG_ENV')

bot = TelegramBot(cfg.main.token)
bot.start()

