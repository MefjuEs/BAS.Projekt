import { __awaiter, __decorate } from "tslib";
import { map, startWith } from 'rxjs/operators';
import { Component, ViewChild } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { FilmCrew } from 'src/app/interfaces/FilmCrew';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
const numberOfItems = 5;
let AddEditMovieComponent = class AddEditMovieComponent {
    constructor(route, movieService, genreService, personnelService) {
        this.route = route;
        this.movieService = movieService;
        this.genreService = genreService;
        this.personnelService = personnelService;
        this.movieTitle = new FormControl('', [Validators.required, Validators.maxLength(100)]);
        this.movieDescription = new FormControl('', [Validators.maxLength(2000)]);
        this.movieReleaseYear = new FormControl('', [Validators.required, Validators.min(1900), Validators.max(2300)]);
        this.movieLengthInMinutes = new FormControl('', [Validators.required, Validators.min(1), Validators.max(1000000)]);
        this.selectedGenres = new FormControl();
        this.selectedActors = [];
        this.selectedDirectors = [];
        this.selectedWriters = [];
        this.actorInputInDropdown = new FormControl();
        this.directorInputInDropdown = new FormControl();
        this.writersInputInDropdown = new FormControl();
        this.separatorKeysCodes = [ENTER, COMMA];
        this.isLoading = true;
        this.notFound = false;
        this.titleExistError = false;
        this.actorsInDropdown = [];
        this.directorsInDropdown = [];
        this.writersInDropdown = [];
        this.actorInputInDropdown.setValue(null);
        this.directorInputInDropdown.setValue(null);
        this.writersInputInDropdown.setValue(null);
        this.filteredActors = this.actorInputInDropdown.valueChanges.pipe(startWith(''), map(value => this._getActorsToDropdown(value)));
    }
    ngOnInit() {
        return __awaiter(this, void 0, void 0, function* () {
            this.actorsInDropdown = yield this.personnelService.getPersonnelToSelectList(numberOfItems, "");
            this.directorsInDropdown = yield this.personnelService.getPersonnelToSelectList(numberOfItems, "");
            this.writersInDropdown = yield this.personnelService.getPersonnelToSelectList(numberOfItems, "");
            this.route.url.subscribe(urlSegments => {
                this.editMode = urlSegments[1].path === 'edit';
                this.genreService.getGenres().subscribe(data => this.genreList = data);
                if (this.editMode === true) {
                    this.getMovie(this.route.snapshot.params.id);
                }
                else {
                    this.movieId = 0;
                    this.movieTitle.setValue('');
                    this.movieDescription.setValue('');
                    this.movieReleaseYear.setValue('');
                    this.movieLengthInMinutes.setValue('');
                    this.file = null;
                    this.updatePhoto = false;
                    this.selectedActors = [];
                    this.selectedDirectors = [];
                    this.selectedWriters = [];
                    this.selectedGenres = new FormControl();
                    this.isLoading = false;
                }
            });
        });
    }
    getMovie(id) {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                let getMovieDTO = yield this.movieService.getMovie(id);
                console.log(getMovieDTO);
                let genres = getMovieDTO.genres.map(g => g.genreId);
                getMovieDTO.personnel.forEach(person => {
                    let personInSelect = {
                        id: person.personId,
                        name: person.name,
                        surname: person.surname
                    };
                    if (person.memberPosition === FilmCrew.Actor) {
                        this.selectedActors.push(personInSelect);
                    }
                    else if (person.memberPosition === FilmCrew.Director) {
                        this.selectedDirectors.push(personInSelect);
                    }
                    else if (person.memberPosition === FilmCrew.Writer) {
                        this.selectedWriters.push(personInSelect);
                    }
                });
                this.movieId = getMovieDTO.id;
                this.movieTitle.setValue(getMovieDTO.title);
                this.movieDescription.setValue(getMovieDTO.description);
                this.movieReleaseYear.setValue(getMovieDTO.releaseYear);
                this.movieLengthInMinutes.setValue(getMovieDTO.movieLengthInMinutes);
                this.file = null;
                this.updatePhoto = false;
                this.selectedGenres.setValue(genres);
                this.isLoading = false;
            }
            catch (exception) {
                this.isLoading = false;
                this.notFound = true;
                console.log('gonwo');
            }
        });
    }
    getTitleErrorMessage() {
        console.log(this.selectedGenres);
        if (this.movieTitle.hasError('required')) {
            return 'Tytuł nie może być pusty';
        }
        if (this.movieTitle.hasError('maxLength')) {
            return 'Tytuł jest za długi';
        }
        if (this.titleExistError) {
            return 'Już istnieje film o takim tytule';
        }
        return '';
    }
    getReleaseYearErrorMessage() {
        if (this.movieReleaseYear.hasError('required')) {
            return 'Rok nie może być pusty';
        }
        if (this.movieReleaseYear.hasError('min')) {
            return 'Rok nie może być mniejszy niż 1900';
        }
        if (this.movieReleaseYear.hasError('max')) {
            return 'Rok nie może być większy niż 2300';
        }
        return '';
    }
    getTimeInMinutesErrorMessage() {
        if (this.movieReleaseYear.hasError('required')) {
            return 'Czas trwania filmu  nie może być pusty';
        }
        if (this.movieReleaseYear.hasError('min')) {
            return 'Czas trwania filmu nie może być mniejszy niż 1 minuta';
        }
        if (this.movieReleaseYear.hasError('max')) {
            return 'Czas trwania filmu nie może być większy niż 1.000.000 minut';
        }
        return '';
    }
    getDescriptionErrorMessage() {
        return this.movieTitle.hasError('maxLength') ? 'Tytuł jest za długi' : '';
    }
    remove(actor) {
        const index = this.selectedActors.indexOf(actor);
        if (index >= 0) {
            this.selectedActors.splice(index, 1);
        }
    }
    add(event) {
        debugger;
        const input = event.input;
        const value = event.value;
        let foundPerson = this.checkIfObjectIsInList(value, this.actorsInDropdown);
        if (foundPerson != null && !this.checkIfActorIsInSelectedList(foundPerson)) {
            this.selectedActors.push(foundPerson);
        }
        // Reset the input value
        if (input) {
            input.value = '';
        }
        this.actorInputInDropdown.setValue(null);
    }
    checkIfObjectIsInList(fullName, list) {
        let foundPerson = null;
        list.forEach(person => {
            let personFullName = (person.name + ' ' + person.surname);
            if (personFullName.startsWith(fullName)) {
                foundPerson = person;
                return;
            }
        });
        return foundPerson;
    }
    checkIfActorIsInSelectedList(actor) {
        if (actor == null) {
            return true;
        }
        let actorExist = false;
        this.selectedActors.forEach(person => {
            if (actor.id == person.id) {
                actorExist = true;
                return;
            }
        });
        return actorExist;
    }
    selected(event) {
        let foundPerson = this.checkIfObjectIsInList(event.option.viewValue, this.actorsInDropdown);
        if (foundPerson != null && !this.checkIfActorIsInSelectedList(foundPerson)) {
            this.selectedActors.push(foundPerson);
        }
        this.actorInput.nativeElement.value = '';
        this.actorInputInDropdown.setValue(null);
    }
    onActorInputChange(value) {
        console.log(value);
    }
    _getActorsToDropdown(value) {
        //console.log("gowno")
        //this.personnelService.getPersonnelToSelectList(numberOfItems, value);
        this.filteredActors.subscribe(data => {
            data = this.actorsInDropdown;
        });
        return this.actorsInDropdown;
    }
};
__decorate([
    ViewChild('actorInput')
], AddEditMovieComponent.prototype, "actorInput", void 0);
__decorate([
    ViewChild('auto')
], AddEditMovieComponent.prototype, "matAutocomplete", void 0);
AddEditMovieComponent = __decorate([
    Component({
        selector: 'app-add-edit-movie',
        templateUrl: './add-edit-movie.component.html',
        styleUrls: ['./add-edit-movie.component.css']
    })
], AddEditMovieComponent);
export { AddEditMovieComponent };
//# sourceMappingURL=add-edit-movie.component.js.map