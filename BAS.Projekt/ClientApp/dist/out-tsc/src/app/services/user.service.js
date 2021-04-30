import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
let UserService = class UserService {
    constructor(http) {
        this.http = http;
        this.url = `${environment.apiUrl}/api/User`;
    }
    getAll() {
        return this.http.get(`${this.url}/users`);
    }
    getById(id) {
        return this.http.get(`${this.url}/users/${id}`);
    }
};
UserService = __decorate([
    Injectable({
        providedIn: 'root'
    })
], UserService);
export { UserService };
//# sourceMappingURL=user.service.js.map