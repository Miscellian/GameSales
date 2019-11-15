import requests

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
        id = self.__getGameIdByName(name)
        app_details = self.__getAppDetailsForGame(id)
        price_overview = app_details['%d' % id]['data']['price_overview']
        return price_overview

        
api = SteamAPI()
price_overview = api.getPriceOverviewForGame("Street Fighter V")
price_message = "Discount: " + str(price_overview['discount_percent']) + "%\n"
price_message += "Price now: " + price_overview['final_formatted'] + "\n"
price_message += "Price without discount: " + price_overview['initial_formatted']
print(price_message)