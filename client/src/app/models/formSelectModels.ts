export interface SelectOption {
    value: string;
    description: string;
}

export const selectType: SelectOption[] = [
    { value: 'all', description: 'Όλοι' },
    { value: 'electricity', description: 'Ρεύμα' },
    { value: 'naturalgas', description: 'Φυσικό Αέριο' },
    { value: 'both', description: 'Ρεύμα - Φυσικό Αέριο' },
]

export const selectStatus: SelectOption[] = [
    { value: 'all', description: 'Όλες' },
    { value: 'approved', description: 'Εγκριθείσες' },
    { value: 'rejected', description: 'Απορριφθείσες' },
    { value: 'pending', description: 'Σε Αναμονή' },
]
