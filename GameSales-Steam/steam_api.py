import requests
import logging
from app_settings import AppSettings

logger = logging.getLogger('steam_logger')
logger.setLevel(logging.DEBUG)
ch = logging.FileHandler('logs/steam_api.log')
ch.setLevel(logging.DEBUG)
formatter = logging.Formatter(
    '%(asctime)s %(module)s:%(lineno)d [%(levelname)s] %(message)s')
ch.setFormatter(formatter)
logger.addHandler(ch)


class SteamAPI:

    def __init__(self):
        self.api_url = "http://api.steampowered.com/"
        self.store_url = "https://store.steampowered.com/"

    def __getGameIdByName(self, name):
        method = "ISteamApps/GetAppList/v0002"
        json = requests.get(self.api_url + method).json()
        for game in json['applist']['apps']:
            if (name == game['name']):
                return game['appid']

    def __getAppDetailsForGame(self, id):
        method = "api/appdetails?appids="
        json = requests.get(self.store_url + method + str(id)).json()
        return json

    def getPriceOverviewForGame(self, name):
        logger.debug(u"Called with args:" + name)
        id = self.__getGameIdByName(name)
        logger.debug(u"Id for game: " + str(id))
        try:
            app_details = self.__getAppDetailsForGame(id)
            price_overview = app_details['%d' % id]['data']['price_overview']
        except TypeError as e:
            logging.debug(e.__str__())
            logging.info(u"Game with this name doesn't exists in Steam")
            price_overview = []
            return price_overview

        logger.info("Game found. id: " + str(id) + ", name: \"" + name + "\"")
        return price_overview
