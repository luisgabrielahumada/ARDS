<template>
    <div class="sidebar-wrapper">
        <nav :class="{sidebar: true, sidebarStatic, sidebarOpened}"
             @mouseenter="sidebarMouseEnter"
             @mouseleave="sidebarMouseLeave">
            <header class="logo">
                <router-link to="/app/dashboard"><span class="primary-word">Menus</span> <span class="secondary-word"></span></router-link>
            </header>
            <h5 class="navTitle first">
                ---
            </h5>
            <ul class="nav">
                <NavLink v-for="row in items" :key="row.id"
                         :activeItem="activeItem"
                         :header="row.header"
                         :link="row.link"
                         :iconName="row.iconName"
                         :index="row.index"
                         :childrenLinks="row.childrenLinks" isHeader />
            </ul>
            <h5 class="navTitle">
                LABELS
            </h5>
            <ul class="sidebarLabels">
                <li>
                    <a href="#">
                        <i class="fa fa-circle text-danger" />
                        <span class="labelName">Core</span>
                    </a>
                </li>
                <li>
                    <a href="#">
                        <i class="fa fa-circle text-primary" />
                        <span class="labelName">UI Elements</span>
                    </a>
                </li>
                <li>
                    <a href="#">
                        <i class="fa fa-circle text-success" />
                        <span class="labelName">Forms</span>
                    </a>
                </li>
            </ul>
            <h5 class="navTitle">
                PROJECTS
            </h5>
            <div class="sidebarAlerts">
                <b-alert v-for="alert in alerts"
                         :key="alert.id"
                         class="sidebarAlert" variant="transparent"
                         show dismissible>
                    <span>{{alert.title}}</span><br />
                    <b-progress class="sidebarProgress progress-xs mt-1"
                                :variant="alert.color" :value="alert.value" :max="100" />
                    <small>{{alert.footer}}</small>
                </b-alert>
            </div>
        </nav>
    </div>
</template>

<script>
    import { mapState, mapActions } from 'vuex';
    import isScreen from '@/core/screenHelper';
    import NavLink from './NavLink/NavLink';
    import { HTTP } from '../../core/Services/http-services';
    export default {
        name: 'Sidebar',
        components: { NavLink },
        data() {
            return {
                alerts: [
                    {
                        id: 0,
                        title: 'Sales Report',
                        value: 15,
                        footer: 'Calculating x-axis bias... 65%',
                        color: 'danger',
                    },
                    {
                        id: 1,
                        title: 'Personal Responsibility',
                        value: 20,
                        footer: 'Provide required notes',
                        color: 'primary',
                    },
                ],
                items: [],
                //items: [
                //    {
                //        header: "Prime",
                //        link: "Prime",
                //        iconName: "flaticon-home",
                //        index: "Prime",
                //        childrenLinks: [
                //            {
                //                header: 'Configuration',
                //                link: 'Prime-Configuration',
                //                index: 'Prime-Configuration-1',
                //                iconName: "flaticon-newtwork",
                //                active: false,
                //                childrenLinks: [
                //                    { header: 'DataBases', link: '/app/configuration/data-bases', index: '1' },
                //                ]
                //            }
                //        ]
                //    },
                //    {
                //        header: "Claro",
                //        link: "Claro",
                //        iconName: "flaticon-home",
                //        index: "Claro",
                //        childrenLinks: [
                //            {
                //                header: 'Configuration',
                //                link: 'Claro-Configuration',
                //                index: 'Claro-Configuration-1',
                //                iconName: "flaticon-home",
                //                active: false,
                //                childrenLinks: [
                //                    { header: 'DataBases', link: '/app/configuration/data-bases' },
                //                ]
                //            },
                //            {
                //                header: 'Parameters',
                //                link: 'Claro-Parameters',
                //                index: 'Claro-Parameters-2',
                //                iconName: "flaticon-home",
                //                active: false,
                //                childrenLinks: [
                //                    { header: 'Charts', link: '/app/components/charts' },
                //                    { header: 'Icons', link: '/app/components/icons' },
                //                    { header: 'Maps', link: '/app/components/maps' }
                //                ]
                //            }
                //        ]
                //    },
                //],
            };
        },
        methods: {
            ...mapActions('layout', ['changeSidebarActive', 'switchSidebar']),
            setActiveByRoute() {
                const paths = this.$route.fullPath.split('/');
                paths.pop();
                this.changeSidebarActive(paths.join('/'));
            },
            sidebarMouseEnter() {
                if (!this.sidebarStatic && (isScreen('lg') || isScreen('xl'))) {
                    this.switchSidebar(false);
                    this.setActiveByRoute();
                }
            },
            sidebarMouseLeave() {
                if (!this.sidebarStatic && (isScreen('lg') || isScreen('xl'))) {
                    this.switchSidebar(true);
                    this.changeSidebarActive(null);
                }
            },
            Get: function () {
                let id = localStorage.getItem("auth_userId");
                return HTTP.post('Menu/GetAccess', { id: id })
                    .then(response => {
                        this.items = response.data.data;
                        //console.log(this.items);
                    })
                    .catch(e => {
                        this.$toasted.error(e.response.data.message);
                    });
            },
        },
        created() {
            this.setActiveByRoute();
        },
        computed: {
            ...mapState('layout', {
                sidebarStatic: state => state.sidebarStatic,
                sidebarOpened: state => !state.sidebarClose,
                activeItem: state => state.sidebarActiveElement,
            }),
        },
        mounted() {
            this.Get();
        }
    };
</script>

<!-- Sidebar styles should be scoped -->
<style src="./Sidebar.scss" lang="scss" scoped />
