<template>
    <div>
        <b-row>
            <b-col>
                <hr />
                <form enctype="multipart/form-data">
                    <b-form-group label-size="sm">
                        <b-form-file id="file-input" ref="file-input" size="sm" placeholder="Choose a file or drop it here..." drop-placeholder="Drop file here..." v-model="file" @change="handleFileUpload($event)"></b-form-file>
                    </b-form-group>
                </form>
            </b-col>
        </b-row>
        <b-row>
            <b-col v-if="items.length">
                <b-card title="List of Files" class="mb-2">
                    <table class="table table-striped table-bordered table-lg mb-0 requests-table">
                        <thead>
                            <tr>
                                <th class="hidden-sm-down">#</th>
                                <th>Name</th>
                                <th>Content Type</th>
                                <th>File</th>
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
                                        <div v-case="'image/jpeg'"  class="fa fa-file-image-o"></div>
                                        <div v-case="'text/plain'" class="fa fa-file-code-o"></div>
                                        <div v-case="'application/pdf'" class="fa fa-file-pdf-o"></div>
                                        <div v-case="' application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'" class="fa fa-file-excel-o"></div>
                                        <div v-case="' application/vnd.openxmlformats-officedocument.wordprocessingml.document'" class="fa fa-file-word-o"></div>
                                        <div v-default class="glyphicon glyphicon-file"></div>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>
<script>
    import { HTTP } from '@/core/Services/http-services';
    import { vSwitch, vCase, vDefault } from 'v-switch-case'
    export default {
        name: 'UploadFile',
        directives: {
            'switch': vSwitch,
            'case': vCase,
            'default': vDefault
        },
        props: {
            id: [String, Number],
            tablename: String,
            props: ['submitFile'],
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
                }
            }
        },
        watch: {

        },
        methods: {
            handleFileUpload(event) {
                this.items.push(event.target.files[0]);
                this.$refs['file-input'].reset();
            },
            submitFile(id, tableName) {
                let formData = new FormData();

                this.items.map(function (value, key) {
                    formData.append("file_" + key, value);
                });


                let config = {
                    headers: {
                        "Content-Type": "multipart/form-data"
                    }
                };
                if (this.items.count > 0) {
                    return HTTP.post('Upload/UploadFile?id=' + id + "&tableName=" + tableName, formData, config)
                        .then(response => {
                            this.loading = false;
                            this.$toasted.success("File upload successful.");
                            this.GetFile(id, tableName);
                        })
                        .catch(e => {
                            //console.log(e.response);
                            this.loading = false;
                            this.$toasted.error(e);
                        });
                }
            },
            GetFile(id, tableName) {
                if (id != 0) {
                    return HTTP.get('Upload/Get?id=' + this.id + "&tableName=" + tableName)
                        .then(response => {
                            this.loading = false;
                            this.items = response.data.data
                        })
                        .catch(e => {
                            console.log(e);
                            this.loading = false;
                            this.$toasted.error(e);
                        })
                }
            },
            RemoveFile(index, file) {
                this.items.splice(index, 1);
                return HTTP.post('Upload/DeleteFile', { id: file.id })
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
            this.GetFile(this.id, this.tablename);
        }
    };
</script>
<style></style>
