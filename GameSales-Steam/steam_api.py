import requests

class SteamAPI:

    def __init__(self):
        self.api_url = "http://api.steampowered.com/"

    def __getGameIdByName(self, name):
        method = "ISteamApps/GetAppList/v0002"
        json = requests.get(self.api_url + method).json()
        for game in json['applist']['apps']:
            if (name == game['name']):
                return game['appid']

    def getNewsForGame(self, name):
        app_id = self.__getGameIdByName(name)
        method = "ISteamNews/GetNewsForApp/v0002/"
        json = requests.get(self.api_url + method + "?appid=%d&count=3&maxlength=300&format=json" % app_id).json()
        news = []
        for new in json['appnews']['newsitems']:
            news.append(new['title'])
        return news
        

api = SteamAPI()
print(api.getNewsForGame("Terraria"))