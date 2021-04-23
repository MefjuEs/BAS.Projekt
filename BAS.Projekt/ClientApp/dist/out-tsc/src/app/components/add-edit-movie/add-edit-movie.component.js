import { __awaiter, __decorate } from "tslib";
import { Component } from '@angular/core';
let AddEditMovieComponent = class AddEditMovieComponent {
    constructor(route, movieService) {
        this.route = route;
        this.movieService = movieService;
        this.isLoding = true;
    }
    ngOnInit() {
        this.route.url.subscribe(urlSegments => {
            this.editMode = urlSegments[1].path === 'edit';
            if (this.editMode === true) {
                this.getMovie(this.route.snapshot.params.id);
            }
            else {
                this.movie = {
                    id: 0,
                    title: '',
                    description: '',
                    releaseYear: 0,
                    movieLengthInMinutes: 0,
                    file: null,
                    updatePhoto: false,
                    crew: [],
                    genres: []
                };
            }
            this.isLoding = false;
        });
    }
    getMovie(id) {
        return __awaiter(this, void 0, void 0, function* () {
            let getMovieDTO = yield this.movieService.getMovie(id);
            console.log(getMovieDTO);
        });
    }
};
AddEditMovieComponent = __decorate([
    Component({
        selector: 'app-add-edit-movie',
        templateUrl: './add-edit-movie.component.html',
        styleUrls: ['./add-edit-movie.component.css']
    })
], AddEditMovieComponent);
export { AddEditMovieComponent };
//# sourceMappingURL=add-edit-movie.component.js.map