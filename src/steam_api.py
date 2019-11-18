import requests
import logging
import logging.config
import yaml

with open("config/logging.yml", 'r') as logfile:
    config = yaml.safe_load(logfile.read())
logging.config.dictConfig(config)

logger = logging.getLogger('steam_logger')

class SteamAPI:

    def __init__(self):
        self.api_url = "http://api.steampowered.com/"
        self.store_url = "https://store.steampowered.com/"
        logger.info(u"Steam API Service initialized")

    def __getGameIdByName(self, name):
        method = "ISteamApps/GetAppList/v0002"
        json = requests.get(self.api_url + method).json()
        for game in json['applist']['apps']:
            if (name.lower() == game['name'].lower()):
                return game['appid']

    def __getAppDetailsForGame(self, id):
        method = "api/appdetails?appids="
        json = requests.get(self.store_url + method + str(id)).json()
        return json

    def getPriceOverviewForGame(self, name):
        logger.debug(u"Called with args: " + name)
        id = self.__getGameIdByName(name)
        logger.debug(u"Id for game: " + str(id))
        try:
            app_details = self.__getAppDetailsForGame(id)
            price_overview = app_details['%d' % id]['data']['price_overview']
        except TypeError as e:
            logger.debug(e.__str__())
            logger.info(u"Game with this name doesn't exists in Steam")
            price_overview = []
            return price_overview

        logger.info("Game found. id: " + str(id) + ", name: \"" + name + "\"")
        return price_overview
