import { __decorate } from "tslib";
import { Component } from '@angular/core';
let HomeComponent = class HomeComponent {
    constructor(moviesService) {
        this.moviesService = moviesService;
        this.movies = {
            currentPage: 1,
            pageSize: 9,
            allPages: 1,
            allElements: 0,
            movieList: []
        };
        this.movieFilters = {
            title: '',
            releaseYearFrom: null,
            releaseYearTo: null,
            movieLengthFrom: null,
            movieLengthTo: null,
            avgRatingFrom: null,
            avgRatingTo: null,
            page: 0,
            pageSize: 9,
            orderBy: '',
            genreId: null
        };
        this.isLoading = true;
        this.isLoadingMoviePage = false;
    }
    ngOnInit() {
        this.getMovies();
    }
    getMovies() {
        this.movieFilters.page += 1;
        this.moviesService.getMovies(this.movieFilters).subscribe(data => {
            data.movieList.forEach(movie => {
                this.movies.movieList.push(movie);
            });
            this.movies.allPages = data.allPages;
            this.isLoading = false;
            this.isLoadingMoviePage = false;
        });
    }
    getMoviePoster(poster) {
        if (poster != null) {
            return `data:${poster.contentType};base64,${poster.file}`;
        }
        else {
            return `/assets/images/noMovieImage.png`;
        }
    }
    onApplyFilters(event) {
        let pageSize = this.movieFilters.pageSize;
        this.movieFilters = event;
        this.movieFilters.pageSize = pageSize;
        this.moviesService.getMovies(this.movieFilters).subscribe(data => this.movies = data);
    }
    onWindowScroll(event) {
        let pos = (document.documentElement.scrollTop || document.body.scrollTop) + document.documentElement.offsetHeight;
        let max = document.documentElement.scrollHeight;
        if (pos == max && this.movies.allPages > this.movieFilters.page) {
            this.isLoadingMoviePage = true;
            this.getMovies();
        }
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