<template>
    <li v-if="!childrenLinks && isHeader" :class="{headerLink: true, className}">
        <router-link :to="link" class="sidebar-link" v-if="sourcePrime">
            <span class="icon">
                <i :class="fullIconName"></i>
            </span>
            {{header}} <sup v-if="label" :class="'text-' + labelColor" class="headerLabel">{{label}}</sup>
            <b-badge v-if="badge" variant="primary" pill>{{badge}}</b-badge>
        </router-link>
        <router-link class="sidebar-link" v-else>
            <span class="icon">
                <i :class="fullIconName"></i>
            </span>
            {{header}} <sup v-if="label" :class="'text-' + labelColor" class="headerLabel">{{label}}</sup>
            <b-badge v-if="badge" variant="primary" pill>{{badge}}</b-badge>
        </router-link>
    </li>
    <li v-else-if="childrenLinks" :class="{headerLink: true, className}">
        <div @click="() => togglePanelCollapse(link)">
            <router-link :to="link" event="" class="d-flex sidebar-link">
                <span class="icon">
                    <i :class="fullIconName"></i>
                </span>
                {{header}} <sup v-if="label" :class="'text-' + labelColor" class="ml-1 headerLabel">{{label}}</sup>&nbsp;
                <div :class="{caretWrapper: true, carretActive: isActive}">
                    <i class="fa fa-angle-right" />
                </div>
            </router-link>
        </div>
        <b-collapse :id="'collapse' + index" :visible="isActive">
            <ul class="sub-menu">
                <li v-for="item in childrenLinks" :class="{headerLink: true, className}">
                    <div @click="() => toggleItemPanelCollapse(item)" style="        overflow: hidden">
                        <router-link :to="item.link" event="" class="d-flex sidebar-link" v-if="item.childrenLinks.length!=0">
                            <i class="fa fa-database" /> {{item.header}}&nbsp;&nbsp;<sup v-if="label" :class="'text-' + labelColor" class="ml-1 headerLabel">{{item.label}}</sup>
                            <div :class="{caretWrapper: true, carretActive: isActive}">
                                <i class="fa fa-angle-right" />
                            </div>
                        </router-link>
                        <router-link :to="item.link" class="d-flex sidebar-link" v-else>
                            <i class="fa fa-database" /> {{item.header}}&nbsp;&nbsp;<sup v-if="label" :class="'text-' + labelColor" class="ml-1 headerLabel">{{item.label}}</sup>
                        </router-link>
                    </div>
                    <b-collapse :id="'collapse-' + item.index" :visible="item.active" v-if="item.childrenLinks.length!=0">
                        <ul class="sub-menu">
                            <NavLink v-for="(link, index) in item.childrenLinks" :key="index"
                                     :activeItem="link.activeItem"
                                     :header="link.header"
                                     :index="link.index"
                                     :link="link.link"
                                     :sourcePrime="link.sourcePrime"
                                     :id="link.id"
                                     :childrenLinks="link.childrenLinks" />
                        </ul>
                    </b-collapse>
                </li>
            </ul>
        </b-collapse>
    </li>
    <li v-else>
        <div @click="() => GetItemMenu(id)" v-if="sourcePrime==0">
            <router-link to="/">
                - {{header}}<sup v-if="label" :class="'text-' + labelColor" class="headerLabel">{{label}}</sup>
            </router-link>
        </div>
        <div v-else>
            <router-link :to="index !== 'menu' && link">
                - {{header}}<sup v-if="label" :class="'text-' + labelColor" class="headerLabel">{{label}}</sup>
            </router-link>
        </div>
    </li>
</template>

<script>
    import { mapActions } from 'vuex';
    import { HTTP } from '../../../core/Services/http-services';
    export default {
        name: 'NavLink',
        props: {
            badge: { type: String, default: '' },
            header: { type: String, default: '' },
            iconName: { type: String, default: '' },
            c: { type: String, default: '' },
            headerLink: { type: String, default: '' },
            link: { type: String, default: '' },
            childrenLinks: { type: Array, default: null },
            className: { type: String, default: '' },
            isHeader: { type: Boolean, default: false },
            deep: { type: Number, default: 0 },
            activeItem: { type: String, default: '' },
            label: { type: String },
            labelColor: { type: String, default: 'warning' },
            index: { type: String },
            sourcePrime: { type: Number, default: 0 },
            id: { type: Number },
        },
        data() {
            return {
                headerLinkWasClicked: true,
                headerItemLinkWasClicked: false,
                rowMenu: null
            };
        },
        methods: {
            ...mapActions('layout', ['changeSidebarActive']),
            togglePanelCollapse(link) {
                this.changeSidebarActive(link);
                this.headerLinkWasClicked = !this.headerLinkWasClicked || !this.activeItem.includes(this.index);
            },
            toggleItemPanelCollapse(index) {
                index.active = !index.active;
                this.headerItemLinkWasClicked = index.active;
            },
            GetItemMenu(id) {
                return HTTP.post('Menu/GetItemMenu', { id: id, userid: localStorage.getItem("auth_userId") })
                    .then(response => {
                        this.rowMenu = response.data.data;
                        console.log(this.rowMenu);
                        //console.log(this.rowMenu);
                        if (this.rowMenu == null || this.rowMenu == undefined || this.rowMenu.autorized == 0) {
                            this.$router.push('/system/not-authorized');
                            return;
                        }
                        this.$router.push(this.rowMenu.path + '?token=' + this.rowMenu.token + "&api=" + this.rowMenu.api + "&url=" + this.rowMenu.urlApi);
                        return;
                    })
                    .catch(e => {
                        this.$toasted.error(e.response.data);
                    });
            },
        },
        computed: {
            fullIconName() {
                return `fi ${this.iconName}`;
            },
            isActive() {
                return (this.activeItem
                    && this.activeItem.includes(this.index)
                    && this.headerLinkWasClicked);
            },
            isActiveItem() {
                return this.headerItemLinkWasClicked;
            },
        },
    };
</script>
<!---->
<style src="./NavLink.scss" lang="scss" scoped />

