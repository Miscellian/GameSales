from bot import TelegramBot
import yaml

with open("config/config.yml", 'r') as ymlfile:
    cfg = yaml.load(ymlfile, Loader=yaml.FullLoader)

bot = TelegramBot(cfg['token'])
bot.start()
bot.stop()