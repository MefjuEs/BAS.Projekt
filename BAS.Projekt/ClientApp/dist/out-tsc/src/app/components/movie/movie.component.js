import { __awaiter, __decorate } from "tslib";
import { Component } from '@angular/core';
import { FilmCrew } from 'src/app/interfaces/FilmCrew';
let MovieComponent = class MovieComponent {
    constructor(route, movieService) {
        this.route = route;
        this.movieService = movieService;
        this.isLoading = true;
        this.notFound = false;
        this.movie = {
            id: 0,
            title: '',
            description: '',
            releaseYear: 0,
            movieLengthInMinutes: 0,
            averageRating: 0,
            moviePoster: null,
            genres: [],
            personnel: [],
            reviews: []
        };
        this.actors = [];
        this.directors = [];
        this.writers = [];
    }
    ngOnInit() {
        this.getMovie(this.route.snapshot.params.id);
    }
    getMovie(id) {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                this.isLoading = true;
                this.movie = yield this.movieService.getMovie(id);
                this.splitMoviePersonnel(this.movie.personnel);
                this.isLoading = false;
            }
            catch (exception) {
                this.isLoading = false;
                this.notFound = true;
            }
        });
    }
    getMoviePoster(poster) {
        if (poster != null) {
            return `data:${poster.contentType};base64,${poster.file}`;
        }
        else {
            return '';
        }
    }
    getMovieLengthInHours(movieLength) {
        let hours = Math.floor(movieLength / 60);
        let minutes = movieLength % 60;
        let movieLengthInHours = hours + "h " + minutes + "min ";
        return movieLengthInHours;
    }
    splitMoviePersonnel(personnel) {
        personnel.forEach(person => {
            if (person.memberPosition == FilmCrew.Actor) {
                this.actors.push(person);
            }
            else if (person.memberPosition == FilmCrew.Director) {
                this.directors.push(person);
            }
            else {
                this.writers.push(person);
            }
        });
        console.log(this.actors);
        console.log(this.directors);
        console.log(this.writers);
    }
};
MovieComponent = __decorate([
    Component({
        selector: 'app-movie',
        templateUrl: './movie.component.html',
        styleUrls: ['./movie.component.css']
    })
], MovieComponent);
export { MovieComponent };
//# sourceMappingURL=movie.component.js.map