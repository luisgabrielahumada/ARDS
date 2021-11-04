import axios from 'axios';
export const HTTP = axios.create({
    baseURL: "https://api-business-manager.azurewebsites.net/api/",
    headers: {
        "Cache-Control": "no-cache",
        "Access-Control-Allow-Origin": "*",
    }
});
HTTP.interceptors.request.use(function (config) {
    const access_token = localStorage.getItem('access_token');
    if (access_token) {
        config.headers.common = {
            "Authorization": `Bearer ${access_token}`,
            "Accept": "application/json, text/plain, */*",
            "Content-Type": "application/json"
        };
    }
    return config;
}, function (err) {
    return Promise.reject(err);
});