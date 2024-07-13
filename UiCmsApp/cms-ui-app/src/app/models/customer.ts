export interface Customer
{
    customerId:number;
    firstName:string;
    lastName:string;
    email:string;
    phone?:string;
    address:string;
    createdBy?:number;
    createdAt:string;
    updatedBy?:number;
    updatedAt?:string;
    
}

export interface CustomerInput
{
    firstName?:string;
    lastName?:string;
    email?:string;
    phone?:string;
    address?:string;
    createdBy?:number;
    
}
