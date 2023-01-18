export class UserParams {
    pageSize = 8;
    pageNumber = 1;
    type = '';
    status = '';
    minMonth = 1;
    maxMonth = (new Date()).getMonth()
    minYear = 2022
    maxYear = (new Date()).getFullYear()
    orderBy = '';
}