<template>
    <div>
        <Widget>
            <b-overlay :show="isloading" rounded="sm">
                <b-row>
                    <b-col>
                        <h2>{{model.name}}</h2>
                    </b-col>
                    <b-col class="text-right">
                        <b-button-group vertical>
                            <b-button @click="$router.go(-1)" variant="primary">Regresa</b-button>
                            <b-button :href="sheet" variant="primary" target="blank">Ver Ficha Tecnica</b-button>
                            <b-button v-b-modal.modal-prevent-closing variant="primary">Editar</b-button>
                        </b-button-group>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col>
                        <div>
                            <b-carousel :interval="4000"
                                        controls
                                        indicators
                                        img-blank
                                        background="#ababab"
                                        img-width="580"
                                        img-height="580"
                                        @sliding-start="onSlideStart"
                                        @sliding-end="onSlideEnd">

                                <b-carousel-slide class="center" v-for="image in images" :img-src=image style="img:{display: block; width: 580px; height: 580px; }"></b-carousel-slide>
                            </b-carousel>
                        </div>
                    </b-col>
                    <b-col>
                        <h2> Detalles</h2>
                        <div>
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <th>Name</th>
                                        <td>{{details[0].name}}</td>
                                    </tr>
                                    <tr>
                                        <th>Description</th>
                                        <td>{{details[0].description}}</td>
                                    </tr>
                                    <tr>
                                        <th>Category</th>
                                        <td>{{details[0].category}}</td>
                                    </tr>
                                    <tr>
                                        <th>Group</th>
                                        <td>{{details[0].group}}</td>
                                    </tr>
                                    <tr>
                                        <th>Cod_Ean</th>
                                        <td>{{details[0].cod_Ean}}</td>
                                    </tr>
                                    <tr>
                                        <th>High</th>
                                        <td>{{details[0].high}}</td>
                                    </tr>
                                    <tr>
                                        <th>Long</th>
                                        <td>{{details[0].long}}</td>
                                    </tr>
                                    <tr>
                                        <th>Width</th>
                                        <td>{{details[0].width}}</td>
                                    </tr>
                                    <tr>
                                        <th>Weight</th>
                                        <td>{{details[0].weight}}</td>
                                    </tr>
                                    <tr>
                                        <th>Unit of weight</th>
                                        <td>{{details[0].unit_of_weight}}</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col>
                        <h2 v-if="getStocts()"> Existencias</h2>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col>
                        <div>
                            <b-table :items="stocks">
                                <template #table-caption>
                                </template>
                            </b-table>
                        </div>
                    </b-col>
                </b-row>
            </b-overlay>
        </Widget>
        <b-modal size="xl" id="modal-prevent-closing" ref="modal-prevent-closing" title="Editar SKU" hide-footer>
            <template #modal-title>
                <b>{{model.name}}</b>
            </template>
            <b-overlay :show="isloading" rounded="sm">
                <b-card>
                    <b-card-body>
                        <b-row>
                            <b-col md="12" xl="12" sm="12" xs="12">
                                <b-form-group label="Nombre Parte" label-for="name-input">
                                    <b-form-input id="name-input" v-model="model.name" :state="validateState('name')" placeholder="Nombre Parte" required></b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="12" xl="12" sm="12" xs="12">
                                <b-form-group label="Descripcion" label-for="description-input">
                                    <b-form-textarea id="description-input" v-model="model.description" :state="validateState('description')" placeholder="Descripcion" required>

                                    </b-form-textarea>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="12" xl="12" sm="12" xs="12">
                                <b-tabs>
                                    <b-tab title="Img">
                                        <upload-File :id="$route.query.id" :tablename="'Product-img'" :multifile="multifile" :token="$route.query.token" :islocal="true"  ref="uploadFile"></upload-File>
                                    </b-tab>
                                </b-tabs>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="12" xl="12" sm="12" xs="12">
                                <div class="float-right">
                                    <b-button variant="success" id="Save" @click="Save">Save</b-button>
                                </div>
                            </b-col>
                        </b-row>
                    </b-card-body>
                </b-card>
            </b-overlay>
        </b-modal>
    </div>

</template>

<script>
    import Widget from '@/components/Widget/Widget';
    import { HTTP } from '@/core/Services/http-services';
    import { required } from "vuelidate/lib/validators";
    import uploadFile from '../_System/Common/Shared/UploadFile';
    export default {
        name: 'Product',
        components: {
            Widget,
            uploadFile
        },
        data() {
            return {
                isloading: false,
                details: [{ name: '' }],
                stocks: [],
                model: {
                    description: '',
                    name: '',
                    id: 0,
                },
                setting: {
                    image: {
                        url: '',
                        sheet: '',
                    },
                },
                sheet: '',
                images: [],
                multifile: false,
            }
        },
        validations: {
            model: {
                name: {
                    required
                },
                description: {
                    required
                }
            }
        },
        computed: {

        },
        methods: {

            onSlideStart(slide) {
                this.sliding = true
            },
            onSlideEnd(slide) {
                this.sliding = false
            },
            getStocts() {
                return (this.stocks.length == 0 ? false : true);
            },
            getFile() {

                for (let detail of this.details) {
                    if (detail.contentType != null) {
                        if (detail.contentType.includes("image"))
                            this.images.push(detail.url)
                        else
                            this.sheet = detail.url
                    }
                }

            },
            getPdfUrl() {
                return "https://drive.google.com/file/d/1TN2NkjjZuCIxtER7Nzw1zfQV7QTqaUvf/view";
            },
            GetSettings: function () {
                this.isloading = true;
                return HTTP.get(`${this.$route.query.url}Products/GetSettings`)
                    .then(response => {
                        this.setting = response.data.data;
                        this.isloading = false;
                    })
                    .catch(e => {
                        this.$toasted.error(e);
                        this.isloading = false;
                    });
            },
            validateState(name) {
                const { $dirty, $error } = this.$v.model[name];
                return $dirty ? !$error : null;
            },
            Existence: function () {
                this.isloading = true;
                return HTTP.get(`${this.$route.query.url}Products/GetExistence?token=` + this.$route.query.token + "&id=" + this.$route.query.id)
                    .then(response => {
                        this.stocks = response.data.data;
                        this.isloading = false;
                    })
                    .catch(e => {
                        this.$toasted.error(e);
                        this.isloading = false;
                    });
            },
            Detail: function () {
                this.isloading = true;
                return HTTP.get(`${this.$route.query.url}Products/GetDetails?token=` + this.$route.query.token + "&id=" + this.$route.query.id)
                    .then(response => {
                        this.details = response.data.data;
                        this.getFile();
                        this.model = { name: this.details[0].name, image: this.details[0].image, description: this.details[0].description };
                        this.isloading = false;
                    })
                    .catch(e => {
                        this.$toasted.error(e);
                        this.isloading = false;
                    });
            },
            Save: function () {
                this.model.id = this.$route.query.id;
                this.isloading = true;
                this.$v.model.$touch();
                if (this.$v.model.$anyError) {
                    this.isloading = false;
                    return;
                }
                return HTTP.post(`${this.$route.query.url}Products/Save?token=`+ this.$route.query.token, this.model)
                    .then(response => {
                        //this.model.id = response.data.data;
                        this.$toasted.success('Successful process.');
                        this.$refs.uploadFile.submitFile(this.model.id, "Product-img", this.$route.query.token,true);
                        this.isloading = false;
                        this.Detail();
                    })
                    .catch(e => {
                        this.$toasted.error(e.response.data);
                        this.isloading = false;
                    });
            },
        },
        mounted() {
            this.Detail();
            this.GetSettings();
            this.Existence();
        }
    }
</script>