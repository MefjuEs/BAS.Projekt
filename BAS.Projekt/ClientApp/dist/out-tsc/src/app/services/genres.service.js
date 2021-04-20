import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
let GenresService = class GenresService {
    constructor(http) {
        this.http = http;
        this.url = 'http://localhost:50927/api/Genre';
    }
    getGenres() {
        return this.http.get(this.url + "/all");
    }
};
GenresService = __decorate([
    Injectable()
], GenresService);
export { GenresService };
//# sourceMappingURL=genres.service.js.map