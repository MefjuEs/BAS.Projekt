import { __awaiter, __decorate } from "tslib";
import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { SnackBarStyle } from 'src/app/interfaces/SnackBarStyle';
let AddEditPersonnelComponent = class AddEditPersonnelComponent {
    constructor(personnelService, notificationService, route, location) {
        this.personnelService = personnelService;
        this.notificationService = notificationService;
        this.route = route;
        this.location = location;
        this.personnelName = new FormControl('', [Validators.required, Validators.maxLength(100)]);
        this.personnelSurname = new FormControl('', [Validators.required, Validators.maxLength(100)]);
        this.personnelNationality = new FormControl('', [Validators.required, Validators.maxLength(100)]);
        this.personnelDateOfBirth = new FormControl(null, [Validators.required]);
        this.minDate = new Date(1800, 1, 1);
        this.maxDate = new Date();
        this.isLoading = true;
        this.notFound = false;
    }
    ngOnInit() {
        this.route.url.subscribe(urlSegments => {
            this.editMode = urlSegments[2].path === 'edit';
            if (this.editMode === true) {
                this.getPerson(this.route.snapshot.params.id);
            }
            else {
                this.personnelId = 0;
                this.personnelName.setValue('');
                this.personnelSurname.setValue('');
                this.personnelNationality.setValue('');
                this.personnelDateOfBirth.setValue(null);
                this.isLoading = false;
            }
        });
    }
    getPerson(id) {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                let person = yield this.personnelService.getPerson(id);
                this.personnelId = person.id;
                this.personnelName.setValue(person.name);
                this.personnelSurname.setValue(person.surname);
                this.personnelNationality.setValue(person.nationality);
                this.personnelDateOfBirth.setValue(new Date(person.dateOfBirth));
                this.isLoading = false;
            }
            catch (exception) {
                this.isLoading = false;
                this.notFound = true;
            }
        });
    }
    getNameErrorMessage() {
        if (this.personnelName.hasError('required')) {
            return 'Imi?? nie mo??e by?? puste';
        }
        if (this.personnelName.hasError('maxLength')) {
            return 'Imi?? jest za d??ugie';
        }
        return '';
    }
    getSurnameErrorMessage() {
        if (this.personnelSurname.hasError('required')) {
            return 'Nazwisko nie mo??e by?? puste';
        }
        if (this.personnelSurname.hasError('maxLength')) {
            return 'Nazwisko jest za d??ugie';
        }
        return '';
    }
    getNationalityErrorMessage() {
        if (this.personnelNationality.hasError('required')) {
            return 'Narodowo???? nie mo??e by?? pusta';
        }
        if (this.personnelNationality.hasError('maxLength')) {
            return 'Narodowo???? jest za d??uga';
        }
        return '';
    }
    onReturn() {
        this.location.back();
    }
    onSubmit() {
        if (this.personnelName.invalid ||
            this.personnelSurname.invalid ||
            this.personnelNationality.invalid ||
            this.personnelDateOfBirth.invalid) {
            this.notificationService.showSnackBarNotification('Nie wszystkie pola s?? poprawne', 'Zamknij', SnackBarStyle.error);
            return;
        }
        let person = {
            id: this.personnelId,
            name: this.personnelName.value,
            surname: this.personnelSurname.value,
            nationality: this.personnelNationality.value,
            dateOfBirth: this.personnelDateOfBirth.value
        };
        if (this.editMode) {
            this.personnelService.editPersonnel(person).subscribe(data => {
                this.notificationService.showSnackBarNotification('Pomy??lnie wprowadzono zmiany', 'Zamknij', SnackBarStyle.success);
                this.location.back();
            });
        }
        else {
            this.personnelService.addPersonnel(person).subscribe(data => {
                this.notificationService.showSnackBarNotification('Pomy??lnie dodano osob??', 'Zamknij', SnackBarStyle.success);
                this.location.back();
            });
        }
    }
};
AddEditPersonnelComponent = __decorate([
    Component({
        selector: 'app-add-edit-personnel',
        templateUrl: './add-edit-personnel.component.html',
        styleUrls: ['./add-edit-personnel.component.css']
    })
], AddEditPersonnelComponent);
export { AddEditPersonnelComponent };
//# sourceMappingURL=add-edit-personnel.component.js.map