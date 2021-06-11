from flask import Flask
from flask_restful import Api
from controllers.recommend.recommend import RecommendController

app = Flask(__name__)
api = Api(app)

api.add_resource(RecommendController, '/movies/recommend')

#parser = argparse.ArgumentParser()
#parser.add_argument("--environment")
#args = parser.parse_args()
#create_config(args.environment)
app.run(host='0.0.0.0')
