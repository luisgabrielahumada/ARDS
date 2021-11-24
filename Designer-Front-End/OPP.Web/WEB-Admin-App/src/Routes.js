import Vue from 'vue';
import Router from 'vue-router';
import Layout from '@/components/Layout/Layout';
import Login from '@/pages/Login/Login';
import ErrorPage from '@/pages/Error/Error';
// Main
import AnalyticsPage from '@/pages/Dashboard/Dashboard';
//System
import DataBases from '@/pages/_System/DataBases';
import DataBase from '@/pages/_System/DataBase';
import authorized from '@/pages/Shared/not-authorized';
//DirectTV
import Products from '@/pages/DirectTV/Products';
import Product from '@/pages/DirectTV/Product';
//WOM
import Georeferencing from '@/pages/Wom/Georeferencing';
import SerialAssignmentPreRecruitment from '@/pages/Wom/SerialAssignmentPreRecruitment';
import TechnicalServiceForm from '@/pages/Wom/TechnicalServiceForm';
//Claro
import Portability from '@/pages/Claro/Portability';

Vue.use(Router);

export default new Router({
    routes: [
        {
            path: '/login',
            name: 'Login',
            component: Login,
        },
        {
            path: '/error',
            name: 'Error',
            component: ErrorPage,
        },
        {
            path: '/system',
            name: 'Layout',
            component: Layout,
            children: [
                {
                    path: 'dashboard',
                    name: 'AnalyticsPage',
                    component: AnalyticsPage,
                },
                {
                    path: 'configuration/data-bases',
                    name: 'DataBases',
                    component: DataBases,
                },
                {
                    path: 'configuration/data-base',
                    name: 'DataBase',
                    component: DataBase,
                },
                {
                    path: 'not-authorized',
                    name: 'authorized',
                    component: authorized,
                },
            ],
        },
        {
            path: '/DirectTV',
            name: 'Layout',
            component: Layout,
            children: [
                {
                    path: 'components/Products',
                    name: 'Products',
                    component: Products,
                },
                {
                    path: 'components/Product',
                    name: 'Product',
                    component: Product,
                },
            ],
        },
        {
            path: '/Wom',
            name: 'Layout',
            component: Layout,
            children: [
                {
                    path: 'dashboard',
                    name: 'AnalyticsPage',
                    component: AnalyticsPage,
                },
                {
                    path: 'components/Georeferencing',
                    name: 'Georeferencing',
                    component: Georeferencing,
                },
                {
                    path: 'components/SerialAssignmentPreRecruitment',
                    name: 'SerialAssignmentPreRecruitment',
                    component: SerialAssignmentPreRecruitment,
                },
                {
                    path: 'components/TechnicalServiceForm',
                    name: 'TechnicalServiceForm',
                    component: TechnicalServiceForm,
                },
            ],
        },
        {
            path: '/Claro',
            name: 'Layout',
            component: Layout,
            children: [
                {
                    path: 'components/Portability',
                    name: 'Portability',
                    component: Portability,
                },
            ],
        },
    ],
});
