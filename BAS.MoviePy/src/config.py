import os
import jsons

config = None

class Config:
    def __init__(self):
        self.fields = None
        self.load_config()

    def load_config(self):
        #to do: add environment variable to choose between development and release
        config_path = os.path.join(os.path.abspath(os.path.join('..', 'appsettings.Development.json')))
        file_handle = open(config_path, "r")
        self.fields = jsons.loads(file_handle.read())

def get_config():
    if config:
        return config
    return Config()
