import { __decorate } from "tslib";
import { Component } from '@angular/core';
let HomeComponent = class HomeComponent {
    constructor(moviesService, genresService) {
        this.moviesService = moviesService;
        this.genresService = genresService;
        this.movieFilters = {
            title: '',
            releaseYearFrom: null,
            releaseYearTo: null,
            movieLengthFrom: null,
            movieLengthTo: null,
            avgRatingFrom: null,
            avgRatingTo: null,
            page: 1,
            pageSize: null,
            orderBy: '',
            genreId: null
        };
    }
    ngOnInit() {
        this.moviesService.getMovies(this.movieFilters).subscribe(data => this.movies = data);
        this.genresService.getGenres().subscribe(data => this.genresList = data);
    }
    getMoviePoster(poster) {
        if (poster != null) {
            return `data:${poster.contentType};base64,${poster.file}`;
        }
        else {
            return '';
        }
    }
    onApplyFilters(event) {
        this.moviesService.getMovies(this.movieFilters).subscribe(data => this.movies = data);
    }
};
HomeComponent = __decorate([
    Component({
        selector: 'home',
        templateUrl: './home.component.html',
        styleUrls: ['./home.component.css']
    })
], HomeComponent);
export { HomeComponent };
//# sourceMappingURL=home.component.js.map