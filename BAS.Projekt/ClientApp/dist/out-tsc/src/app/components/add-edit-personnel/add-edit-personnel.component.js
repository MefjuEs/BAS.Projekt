import { __awaiter, __decorate } from "tslib";
import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
let AddEditPersonnelComponent = class AddEditPersonnelComponent {
    constructor(personnelService, route, dateAdapter) {
        this.personnelService = personnelService;
        this.route = route;
        this.dateAdapter = dateAdapter;
        this.personnelName = new FormControl('', [Validators.required, Validators.maxLength(100)]);
        this.personnelSurname = new FormControl('', [Validators.required, Validators.maxLength(100)]);
        this.personnelNationality = new FormControl('', [Validators.required, Validators.maxLength(100)]);
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
                this.personnelDateOfBirth = null;
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
                this.personnelDateOfBirth = person.dateOfBirth;
                this.isLoading = false;
            }
            catch (exception) {
                this.isLoading = false;
                this.notFound = true;
            }
        });
    }
    getNameErrorMessage() {
        return '';
    }
    getSurnameErrorMessage() {
        return '';
    }
    getNationalityErrorMessage() {
        return '';
    }
    onSubmit() {
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