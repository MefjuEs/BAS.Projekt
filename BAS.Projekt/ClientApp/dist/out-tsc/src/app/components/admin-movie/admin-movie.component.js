import { __decorate } from "tslib";
import { Component } from '@angular/core';
let AdminMovieComponent = class AdminMovieComponent {
    constructor(moviesService, dialog) {
        this.moviesService = moviesService;
        this.dialog = dialog;
        this.movieFilters = {
            title: '',
            releaseYearFrom: null,
            releaseYearTo: null,
            movieLengthFrom: null,
            movieLengthTo: null,
            avgRatingFrom: null,
            avgRatingTo: null,
            page: 1,
            pageSize: 10,
            orderBy: '',
            genreId: null
        };
        this.movies = {
            currentPage: 1,
            pageSize: 10,
            allPages: 1,
            allElements: 0,
            movieList: []
        };
        this.displayedColumns = ['title', 'releaseYear', 'movieLengthInMinutes', 'symbol'];
    }
    ngOnInit() {
        this.pageIndex = 0;
        this.movieFilters.pageSize = 10;
        this.getMovies();
    }
    getMovies() {
        this.moviesService.getMovies(this.movieFilters).subscribe(data => this.movies = data);
    }
    onApplyFilters(event) {
        let pageSize = this.movieFilters.pageSize;
        this.movieFilters = event;
        this.movieFilters.pageSize = pageSize;
        this.movieFilters.page = 1;
        this.pageIndex = 0;
        this.getMovies();
    }
    changePage(event) {
        if (event.pageSize != this.movieFilters.pageSize) {
            this.movieFilters.pageSize = event.pageSize;
        }
        this.pageIndex = event.pageIndex;
        this.movieFilters.page = this.pageIndex + 1;
        this.getMovies();
        console.log(event);
        console.log(this.movieFilters.page);
    }
    openDeleteDialog() {
        debugger;
        console.log('gowno');
        const dialogRef = this.dialog.open(DeleteMovieDialog);
        dialogRef.afterClosed().subscribe(result => {
            console.log(result);
        });
    }
};
AdminMovieComponent = __decorate([
    Component({
        selector: 'admin-movie',
        templateUrl: './admin-movie.component.html',
        styleUrls: ['./admin-movie.component.css']
    })
], AdminMovieComponent);
export { AdminMovieComponent };
let DeleteMovieDialog = class DeleteMovieDialog {
};
DeleteMovieDialog = __decorate([
    Component({
        selector: 'delete-movie-dialog',
        templateUrl: './delete-movie-dialog.html',
    })
], DeleteMovieDialog);
export { DeleteMovieDialog };
//# sourceMappingURL=admin-movie.component.js.map