import Vue from 'vue';
import Router from 'vue-router';

import Layout from '@/components/Layout/Layout';
import Login from '@/pages/Login/Login';
import ErrorPage from '@/pages/Error/Error';
// Core
import TypographyPage from '@/pages/Typography/Typography';

// Tables
import TablesBasicPage from '@/pages/Tables/Basic';

// Maps
import GoogleMapPage from '@/pages/Maps/Google';

// Main
import AnalyticsPage from '@/pages/Dashboard/Dashboard';

// Charts
import ChartsPage from '@/pages/Charts/Charts';

// Ui
import IconsPage from '@/pages/Icons/Icons';
import NotificationsPage from '@/pages/Notifications/Notifications';

//System
import DataBases from '@/pages/_System/DataBases';
import DataBase from '@/pages/_System/DataBase';
import Role from '@/pages/_System/Common/Role';
import Menu from '@/pages/_System/Common/Menu';
import User from '@/pages/_System/Common/User';
import authorized from '@/pages/Shared/not-authorized';

//Prime
import Trucks from '@/pages/Wom/Trucks';


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
                    path: 'configuration/role',
                    name: 'Role',
                    component: Role,
                },
                {
                    path: 'configuration/menu',
                    name: 'Menu',
                    component: Menu,
                },
                {
                    path: 'configuration/user',
                    name: 'User',
                    component: User,
                },
                {
                    path: 'not-authorized',
                    name: 'authorized',
                    component: authorized,
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
                    path: 'components/trucks',
                    name: 'Trucks',
                    component: Trucks,
                },
            ],
        },
        {
            path: '/app',
            name: 'Layout',
            component: Layout,
            children: [
                {
                    path: 'dashboard',
                    name: 'AnalyticsPage',
                    component: AnalyticsPage,
                },
                {
                    path: 'components/typographyPage',
                    name: 'TypographyPage',
                    component: TypographyPage,
                },
                {
                    path: 'components/typography',
                    name: 'TypographyPage',
                    component: TypographyPage,
                },
                {
                    path: 'components/tables',
                    name: 'TablesBasicPage',
                    component: TablesBasicPage,
                },
            ],
        },
    ],
});
