<template>
    <b-overlay :show="isloading" rounded="sm">
        <b-row>
            <b-col>
                <Widget title="">
                    <b-row>
                        <b-col class="text-right">
                            <export-excel class="btn btn-outline-primary"
                                          :data="items"
                                          name="Georeferencing.xls"
                                          :before-finish="updateGeoreferencing">
                                Download Excel
                            </export-excel>
                        </b-col>
                    </b-row>
                    <p></p>
                    <div>
                        <table class="table table-striped table-bordered table-lg mb-0 requests-table">
                            <thead>
                                <tr>
                                    <th>CodMovimiento</th>
                                    <th>Zone</th>
                                    <th>Neighborhood</th>
                                    <th>CodeNeighborhood</th>
                                    <th>Manzana</th>
                                    <th>CodBarFront</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="row in items" :key="row.id">
                                    <td>{{row.cod_movimiento}}</td>
                                    <td>{{row.zone}}</td>
                                    <td>{{row.neighborhood}}</td>
                                    <td>{{row.codeNeighborhood}}</td>
                                    <td>{{row.Manzana}}</td>
                                    <td>{{row.CodBarFront}}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </Widget>
            </b-col>
        </b-row>
    </b-overlay>
</template>

<script>

    import Widget from "@/components/Widget/Widget";
    import { HTTP } from "@/core/Services/http-services";
    import Vue from 'vue'
    import excel from 'vue-excel-export'
    Vue.use(excel)
    export default {
        components: {
            Widget,
        },
        data() {
            return {
                isloading: false,
                items: [],
            }
        },
        mounted() {
            this.getGeoreferencing();
        },
        methods: {
            getGeoreferencing: function () {
                this.isloading = true;
                return HTTP.get(`${this.$route.query.url}Order/getGeoreferencing`)
                    .then((response) => {
                        this.items = response.data.data;
                        this.isloading = false;
                    })
                    .catch((e) => {
                        this.isloading = false;
                        this.$toasted.error(e);
                    });
            },
            updateGeoreferencing: function () {
                this.isloading = true;
                return HTTP.post(`${this.$route.query.url}Order/DownloadedGeoreferencing`)
                    .then((response) => {
                        this.isloading = false;
                        this.getGeoreferencing();
                    })
                    .catch((e) => {
                        this.isloading = false;
                        this.$toasted.error(e);
                    })
            }

        },


    };
</script>
