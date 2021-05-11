import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpHeaders, HttpParams } from '@angular/common/http';
let ReviewService = class ReviewService {
    constructor(http) {
        this.http = http;
        this.url = `${environment.apiUrl}/api/Review`;
    }
    getAllReviews(filters) {
        let headers = new HttpHeaders();
        headers.append('Content-Type', 'application/json');
        let params = new HttpParams();
        params = params.append('movieId', filters.movieId == null ? '' : filters.movieId.toString());
        params = params.append('userId', filters.userId == null ? '' : filters.userId.toString());
        params = params.append('page', filters.page.toString());
        params = params.append('pageSize', filters.pageSize == null ? '' : filters.pageSize.toString());
        params = params.append('orderBy', filters.orderBy);
        return this.http.get(`${this.url}/all`, { headers: headers, params: params });
    }
};
ReviewService = __decorate([
    Injectable({
        providedIn: 'root'
    })
], ReviewService);
export { ReviewService };
//# sourceMappingURL=review.service.js.map