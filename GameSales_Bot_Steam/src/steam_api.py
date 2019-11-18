import requests
import logging
import logging.config
import yaml

with open("config/logging.yml", 'r') as logfile:
    config = yaml.safe_load(logfile.read())
logging.config.dictConfig(config)

logger = logging.getLogger('steam_logger')


class CustomDict(dict):
    """ Dict which allow to access to dict values using dot, e.g. my_dict.key.key1 instead my_dict['key']['key1'] """

    def __getattr__(self, item):
        val = self[item]
        if isinstance(val, dict):
            return CustomDict(val)
        else:
            return val


class SteamAPI:

    def __init__(self):
        self.api_url = "http://api.steampowered.com/"
        self.store_url = "https://store.steampowered.com/"
        logger.info(u"Steam API Service initialized")

    def __getAppDetailsForGame(self, id):
        method = "api/appdetails?appids="
        json = requests.get(self.store_url + method + str(id)).json()
        return json

    def __getGameIdByName(self, name):
        method = "ISteamApps/GetAppList/v0002"
        json = requests.get(self.api_url + method).json()
        for game in json['applist']['apps']:
            if (name.lower() == game['name'].lower()):
                appid = game['appid']
                game_details = self.__getAppDetailsForGame(appid)['%d' % appid]
                if (game_details['success'] == True):
                    return appid, game_details

    def getPriceOverviewForGame(self, name):
        logger.debug(u"Called with args: " + name)
        try:
            id, app_details = self.__getGameIdByName(name)
            logger.debug(u"Id for game: " + str(id))
            price_overview = app_details['data']['price_overview']
        except TypeError as e:
            logger.debug(e.__str__())
            logger.info(u"Game with this name doesn't exists in Steam")
            price_overview = []
            return price_overview

        logger.info("Game found. id: " + str(id) + ", name: \"" + name + "\"")
        return price_overview
