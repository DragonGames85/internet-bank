import 'dotenv/config';

const productionMode = process.env.NEXT_PUBLIC_PRODUCTION_MODE === 'true';
export const authAppUrl =
    (productionMode ? process.env.NEXT_PUBLIC_AUTH_APP_URL_PROD : process.env.NEXT_PUBLIC_AUTH_APP_URL_LOCAL) ?? '';
export const clientAppUrl =
    (productionMode ? process.env.NEXT_PUBLIC_CLIENT_APP_URL_PROD : process.env.NEXT_PUBLIC_CLIENT_APP_URL_LOCAL) ?? '';
export const employeeAppUrl =
    (productionMode ? process.env.NEXT_PUBLIC_EMPLOYEE_APP_URL_PROD : process.env.NEXT_PUBLIC_EMPLOYEE_APP_URL_LOCAL) ?? '';
export const webauthAppUrl =
    (productionMode ? process.env.NEXT_PUBLIC_WEBAUTH_APP_URL_PROD : process.env.NEXT_PUBLIC_WEBAUTH_APP_URL_LOCAL) ?? '';
