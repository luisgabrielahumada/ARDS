<template>
    <div class="auth-page">
        <b-container>
            <!--<div class="btn-wrapper text-center">
                <img src="https://secureservercdn.net/72.167.242.48/yzz.991.myftpupload.com/wp-content/uploads/2020/07/zljs.png?time=1631041975" title="zljs" alt="zljs" style="width:120px">
            </div>-->
            <h5 class="auth-logo">
                <i class="fa fa-circle text-primary"></i>
                Sing App
                <i class="fa fa-circle text-danger"></i>
            </h5>
            <Widget class="widget-auth mx-auto" title="<h3 class='mt-0'>Login to your Web App</h3>" customHeader>
                <form @submit.prevent="Login">
                    <p class="widget-auth-info">
                        Use your email to sign in.
                    </p>
                    <div class="form-group">
                        <input class="form-control no-border" v-model="model.email" type="text" name="email" placeholder="Email" required />
                    </div>
                    <div class="form-group">
                        <input class="form-control no-border" v-model="model.password" type="password" name="password" placeholder="Password" required />
                    </div>
                    <b-button type="submit" size="sm" class="auth-btn mb-3" variant="inverse">Login</b-button>
                    <div v-show="loading" class="text-center"><b-spinner label="Spinning" small></b-spinner></div>
                </form>
            </Widget>
        </b-container>
        <footer class="auth-footer">
            2021 &copy; Template for Dev with vue & ASP.NET Core
        </footer>
    </div>
</template>

<script>
    import Widget from '@/components/Widget/Widget';
    import { HTTP } from '@/core/Services/http-services';
    export default {
        name: 'LoginPage',
        components: { Widget },
        data() {
            return {
                model: {
                    email: '',
                    password: ''
                },
                loading: false,
            };
        },
        methods: {
            Login() {
                this.loading = true;
                return HTTP.post('Auth/Login', { login: this.model.email, password: this.model.password })
                    .then(response => {
                        console.log(response.data.data);
                        localStorage.setItem('access_token', response.data.data.token);
                        localStorage.setItem('auth_username', response.data.data.name);
                        localStorage.setItem('auth_email', response.data.data.email);
                        localStorage.setItem('auth_trasactionDate', response.data.data.transactionDate);
                        localStorage.setItem('auth_sessionId', response.data.data.sessionId);
                        localStorage.setItem('auth_userId', response.data.data.userId);
                        localStorage.setItem('loggedIn', true);
                        this.loading = false;
                        window.localStorage.setItem('authenticated', true);
                        this.$router.push('/system/dashboard');
                    })
                    .catch(e => {
                        console.log(e);
                        this.loading = false;
                        this.$toasted.error(e.response.data.message);
                    })
            },
        },
    };
</script>
