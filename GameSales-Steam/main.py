from steam_api import SteamAPI
api = SteamAPI()
price_overview = api.getPriceOverviewForGame("Terraria")
print(price_overview)