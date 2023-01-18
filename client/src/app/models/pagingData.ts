export interface PagingData {
    currentPage: number;
    itemsPerPage: number;
    totalItems: number;
    totalPages: number;
}

export class PagedList<T> {
    result?: T;
    pagination?: PagingData;
}