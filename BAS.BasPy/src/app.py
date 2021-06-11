import argparse
from flask import Flask
from flask_restful import Api
from controllers.recommend.recommend import RecommendController
from config import create_config

app = Flask(__name__)
api = Api(app)

api.add_resource(RecommendController, '/movies/recommend')

#parser = argparse.ArgumentParser()
#parser.add_argument("--environment")
#args = parser.parse_args()
#create_config(args.environment)
#app.run()
