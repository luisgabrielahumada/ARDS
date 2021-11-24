<template>
    <div>
        <b-row v-if="quantityFile">
            <b-col>
                <form enctype="multipart/form-data">
                    <b-form-group label-size="sm">
                        <b-form-file id="file-input" ref="file-input" size="sm" placeholder="Choose a file or drop it here..." drop-placeholder="Drop file here..." v-model="file" @change="handleFileUpload($event)"></b-form-file>
                    </b-form-group>
                </form>
            </b-col>
        </b-row>
        <b-row v-if="!singlefile">
            <b-col v-if="items.length">
                <b-card title="List of Files" class="mb-2">
                    <table class="table table-striped table-bordered table-lg mb-0 requests-table">
                        <thead>
                            <tr>
                                <th class="hidden-sm-down">#</th>
                                <th>Name</th>
                                <th>Content Type</th>
                                <th>File</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(item, index) in items" :key="item.id">

                                <td>
                                    <b-button variant="link" @click="RemoveFile(index,item)">
                                        <span class="glyphicon glyphicon-trash">
                                        </span>
                                    </b-button>
                                </td>
                                <td>
                                    <a :href="item.url">{{ item.name }}</a>
                                </td>
                                <td>
                                    {{item.contentType}}
                                </td>
                                <td>
                                    <div v-switch="item.contentType">
                                        <!--div v-case="'image/jpeg'"><b-img v-bind="mainProps" :src="GetImageUrl(item.url)" thumbnail fluid alt="Responsive image"></b-img></div-->
                                        <div v-case="'image/jpeg'" class="fa fa-file-image-o"></div>
                                        <div v-case="'text/plain'" class="fa fa-file-code-o"></div>
                                        <div v-case="'application/pdf'" class="fa fa-file-pdf-o"></div>
                                        <div v-case="' application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'" class="fa fa-file-excel-o"></div>
                                        <div v-case="' application/vnd.openxmlformats-officedocument.wordprocessingml.document'" class="fa fa-file-word-o"></div>
                                        <div v-default class="glyphicon glyphicon-file"></div>
                                    </div>
                                </td>
                                <td>
                                    <actions :code="tablename" :id="id"></actions>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </b-card>
            </b-col>
        </b-row>
        <b-row v-else>
            <div v-if="items.length">
                <a :href="items[0].url">{{ items[0].name }}  <span class="fa fa-file-code-o"></span></a>
            </div>
        </b-row>
        <b-modal size="xl" id="modal-Pruebas" hide-footer>
            <template #modal-title>
                <b>Pruebas</b>
            </template>
            <b-overlay rounded="sm">
                Pruebas
            </b-overlay>
        </b-modal>
    </div>
</template>
<script>
    import { HTTP } from '@/core/Services/http-services';
    import { vSwitch, vCase, vDefault } from 'v-switch-case';
    import actions from '@/components/Control/actions';
    export default {
        name: 'UploadFile',
        directives: {
            'switch': vSwitch,
            'case': vCase,
            'default': vDefault
        },
        components: {
            actions
        },
        props: {
            id: [String, Number],
            tablename: String,
            multifile: Boolean,
            islocal: Boolean,
            props: ['submitFile'],
            token: String,
            singlefile: Boolean
        },
        data() {
            return {
                submitted: false,
                loading: false,
                items: [],
                file: null,
                mainProps: {
                    width: 75,
                    height: 75,
                    class: 'm1'
                },
                mainPropsDefault: {
                    blank: true,
                    width: 75,
                    height: 75,
                    class: 'm1'
                },
                quantityFile: true
            }
        },
        watch: {

        },
        methods: {
            handleFileUpload(event) {
                this.items.push(event.target.files[0]);
                this.$refs['file-input'].reset();
            },
            submitFile(id, tableName, token, islocal) {
                let formData = new FormData();

                this.items.map(function (value, key) {
                    formData.append("file_" + key, value);
                });

                let config = {
                    headers: {
                        "Content-Type": "multipart/form-data"
                    }
                };
                if (this.items.length == 0) return;
                if (islocal) {
                    return HTTP.post('Upload/UploadFile?id=' + id + "&tableName=" + tableName + "&islocal=" + islocal, formData, config)
                        .then(response => {
                            this.loading = false;
                            this.$toasted.success("File upload successful.");
                            this.GetFile(id, tableName);
                        })
                        .catch(e => {
                            //console.log(e.response);
                            this.loading = false;
                            this.$toasted.error(e);
                        })
                }
                return HTTP.post('Upload/UploadFile?token=' + token + '&id=' + id + "&tableName=" + tableName + "&islocal=" + islocal, formData, config)
                    .then(response => {
                        this.loading = false;
                        this.$toasted.success("File upload successful.");
                        this.GetFile(id, tableName, islocal);
                    })
                    .catch(e => {
                        //console.log(e.response);
                        this.loading = false;
                        this.$toasted.error(e);
                    })
            },
            GetFile(id, tableName, islocal) {
                if (id != 0) {
                    if (islocal) {
                        return HTTP.get('Upload/Get?id=' + this.id + "&tableName=" + tableName + "&islocal=" + islocal)
                            .then(response => {
                                this.loading = false;
                                this.items = response.data.data;
                                if (this.items.length > 0)
                                    this.quantityFile = !this.multifile && this.items.length >= 1;

                            })
                            .catch(e => {
                                console.log(e);
                                this.loading = false;
                                this.$toasted.error(e);
                            })
                    } else {
                        return HTTP.get('Upload/Get?token=' + this.token + '&id=' + this.id + "&tableName=" + tableName + "&islocal=" + islocal)
                            .then(response => {
                                this.loading = false;
                                this.items = response.data.data;
                                if (this.items.length > 0)
                                    this.quantityFile = !this.multifile && this.items.length >= 1;

                            })
                            .catch(e => {
                                console.log(e);
                                this.loading = false;
                                this.$toasted.error(e);
                            })
                    }
                }
            },
            RemoveFile(index, file) {
                this.items.splice(index, 1);
                return HTTP.post('Upload/DeleteFile?token=' + this.token, { id: file.id })
                    .then(response => {
                        this.loading = false;
                        this.$toasted.success("remove file successfully.");
                    })
                    .catch(e => {
                        console.log(e);
                        this.loading = false;
                        this.$toasted.error(e);
                    })

            },
            GetImageUrl(url) {
                return `${url}`;
            }
        },
        created: function () {
            //this.$parent.$on('saveFile', this.submitFile);
        },
        mounted() {
            this.multi = this.multifile;
            this.GetFile(this.id, this.tablename, this.islocal);
        }
    };
</script>
<style></style>
