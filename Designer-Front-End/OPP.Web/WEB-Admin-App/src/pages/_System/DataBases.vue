<template>
    <div class="tables-basic">
        <h2 class="page-title"><span class="fw-semi-bold">DataBases</span></h2>
        <b-overlay :show="isloading" rounded="sm">
            <b-row>
                <b-col>
                    <Widget title="">
                        <div class="clearfix">
                            <div class="float-right">
                                <b-dropdown variant="inverse" class="mr-xs" size="sm" text="Options" right>
                                    <b-dropdown-item @click="Get(0)">New</b-dropdown-item>
                                    <b-dropdown-item @click="List(paginationAndFilter.pageIndex)">Refresh</b-dropdown-item>
                                </b-dropdown>
                            </div>
                        </div>
                        <div class="table-resposive">
                            <table class="table table-striped table-bordered table-lg mb-0 requests-table">
                                <thead>
                                    <tr>
                                        <th class="hidden-sm-down">#</th>
                                        <th>Name</th>
                                        <th>Alias</th>
                                        <th>ConnectionString</th>
                                        <th>Storage</th>
                                        <th>Source</th>
                                        <th class="hidden-sm-down">Status</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="row in items" :key="row.id">
                                        <td>
                                            <a class="d-flex sidebar-link" @click="Get(row.id)">
                                                <span class="glyphicon glyphicon-edit">
                                                </span>
                                            </a>
                                        </td>
                                        <td>
                                            {{row.name}}
                                        </td>
                                        <td>
                                            {{row.alias}}
                                        </td>
                                        <td>
                                            {{row.connectionString}}
                                        </td>
                                        <td>
                                            {{row.storage}}
                                        </td>
                                        <td>
                                            <div v-if="row.databaseType == 1" class="fa fa-database"> SQL Server</div>
                                            <div v-if="row.databaseType == 2" class="fa fa-database"> MySQL Server</div>
                                            <div v-if="row.databaseType == 3" class="fa fa-soundcloud"> API</div>
                                            <div v-if="row.databaseType == 0" class="fa fa-file-code-o"> No Configuration</div>
                                        </td>
                                        <td class="width-150">
                                            <b-form-checkbox class="mb-2 mr-sm-2 mb-sm-0" v-model="row.status" value="true" unchecked-value="false" disabled switch></b-form-checkbox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="clearfix">
                            <div class="float-right">
                                <b-pagination v-model="paginationAndFilter.pageIndex"
                                              :total-rows="paginationAndFilter.pageTotals"
                                              :per-page="paginationAndFilter.pageSize"
                                              first-number
                                              last-number
                                              @change="List">
                                </b-pagination>
                            </div>
                        </div>
                    </Widget>
                </b-col>
            </b-row>
            </b-overlay>
    </div>
</template>

<script>
    import Widget from '@/components/Widget/Widget';
    import { HTTP } from '@/core/Services/http-services';
    import { vSwitch, vCase, vDefault } from 'v-switch-case';
    export default {
        name: 'DataBases',
        components: {
            Widget,
            'switch': vSwitch,
            'case': vCase,
            'default': vDefault
        },
        data() {
            return {
                isloading: true,
                items: [],
                paginationAndFilter: {
                    pageIndex: 1,
                    pageSize: 10,
                    pageTotals: 1,
                }
            };
        },
        methods: {
            List: function (pageIndex) {
                this.paginationAndFilter.pageIndex = pageIndex;
                return HTTP.post('DataBase/List', this.paginationAndFilter)
                    .then(response => {
                        this.items = response.data.data.items
                        this.paginationAndFilter.pageTotals = response.data.data.totalItemCount;
                        this.isloading = false;
                    })
                    .catch(e => {
                        this.$toasted.error(e);
                        this.isloading = false;
                    });
            },
            Get: function (id) {
                this.$router.push('/system/configuration/data-base?id=' + id);
            },
            State: function (id, status) {
                return HTTP.post('DataBase/State', { id: id, status: status })
                    .then(response => {
                        this.$toasted.success(response.message);
                    })
                    .catch(e => {
                        this.$toasted.error(e.response.data);
                    });
            },
        },
        mounted() {
            this.List(this.paginationAndFilter.pageIndex);
        }
    };
</script>
