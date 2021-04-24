import { __awaiter, __decorate } from "tslib";
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpParams } from '@angular/common/http';
let PersonnelService = class PersonnelService {
    constructor(http) {
        this.http = http;
        this.url = 'http://localhost:50927/api/Personnel';
    }
    getPersonnelToSelectList(numberOfItems, fullName) {
        return __awaiter(this, void 0, void 0, function* () {
            let headers = new HttpHeaders();
            headers = headers.append('Content-Type', 'application/json');
            let params = new HttpParams();
            params = params.append('numberOfItems', numberOfItems.toString());
            params = params.append('fullName', fullName);
            return yield this.http.get(`${this.url}/select`, { headers: headers, params: params }).toPromise();
        });
    }
};
PersonnelService = __decorate([
    Injectable()
], PersonnelService);
export { PersonnelService };
//# sourceMappingURL=personnel.service.js.map