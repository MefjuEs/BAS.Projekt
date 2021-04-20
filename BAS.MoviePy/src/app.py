from flask import Flask
from flask_restful import Api
from controllers.recommend.recommend import RecommendController

app = Flask(__name__)
api = Api(app)

api.add_resource(RecommendController, '/movies/recommend')

if __name__ == '__main__':
    app.run(debug=True, port=5000)
