export class OptionObject {
    name?: string;
    value?: any;
    data?: any;
    constructor(name?: string, value?: any, data?:any) {
        this.name = name;
        this.value = value;
        this.data = data;
    }
}