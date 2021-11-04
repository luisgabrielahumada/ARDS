<template>
    <div class="tables-basic">
        <h2 class="page-title"><span class="fw-semi-bold">Trucks</span></h2>
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
                                        <th>plaque</th>
                                        <th>capacity</th>
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
                                            {{row.plaque}}
                                        </td>
                                        <td>
                                            {{row.capacity}}
                                        </td>
                                        <td class="width-150">
                                            <b-form-checkbox class="mb-2 mr-sm-2 mb-sm-0" v-model="row.status" value="1" disabled unchecked-value="0" switch></b-form-checkbox>
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
    export default {
        name: 'DataBases',
        components: { Widget },
        data() {
            return {
                isloading: false,
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
                this.isloading = true;
                this.paginationAndFilter.pageIndex = pageIndex;
                return HTTP.post('Truck/List?token=' + this.$route.query.token, this.paginationAndFilter)
                    .then(response => {
                        this.items = response.data.data.items
                        this.isloading = false;
                        this.paginationAndFilter.pageTotals = response.data.data.totalItemCount;
                    })
                    .catch(e => {
                        this.$toasted.error(e);
                        this.isloading = false;
                    });
            },
        },
        mounted() {
            this.List(this.paginationAndFilter.pageIndex);
        }
    };
</script>
