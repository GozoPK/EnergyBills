export class UserParams {
    pageSize = 8;
    pageNumber = 1;
    type = '';
    status = '';
    minMonth = 1;
    maxMonth = (new Date()).getMonth() + 1
    minYear = 2022
    maxYear = (new Date()).getFullYear()
    orderBy = '';
    state = ''
}