<template>
    <div class="home">
        <h1>{{ msg }}</h1>
        <b-button to="/newBook" variant="primary">New Book</b-button>
        <br> <br>
        <b-form-input v-on:input="searchBooksByTitle" v-model="titleSearchString" placeholder="Serach catalog by title..."></b-form-input>
        <br>
        <b-form-input type="text" v-on:input="searchBooksByDescription" v-model="descriptionSearchString" placeholder="Serach catalog by description..."></b-form-input>
        <BookTable :bookList="bookList"></BookTable>
        <span>
            <b-link v-on:click="navigateToPreviousPage"> Previous Page </b-link> 
            | <input type="number" min="1" v-model="newPageNum" @keyup.enter="navigateToPage"> |
            <b-link v-on:click="navigateToNextPage"> Next Page </b-link>
        </span>
    </div>
</template>

<script>
    import axios from 'axios';
    import BookTable from './BookTable.vue';

    export default {
        name: 'Home',
        components: {
            'BookTable': BookTable
        },
        props: {
            msg: String,
        },
        data: () => ({
            currentPage: 1,
            titleSearchString: "",
            descriptionSearchString: "",
            hasNextPage: false,
            isSearchByTitle: Boolean,
            isSearchByDescription: Boolean,
            bookList: []
        }),
        methods: {
            searchBooksByTitle() {
                this.isSearchByTitle = false;
                if (this.titleSearchString.length > 0) {
                    this.isSearchByTitle = true;
                } 
                this.currentPage = 1; this.newPageNum = 1;
                this.descriptionSearchString = "";
                this.isSearchByDescription = false;
                this.getData();
            },
            searchBooksByDescription() {
                this.isSearchByDescription = false;
                if (this.descriptionSearchString.length > 0) {
                    this.isSearchByDescription = true;
                } 
                this.currentPage = 1; this.newPageNum = 1;
                this.titleSearchString = ""
                this.isSearchByTitle = false;
                this.getData();
            },
            getData() {
                let urlString = `?pageNumber=${this.currentPage}`;
                if (this.isSearchByTitle) {
                    urlString = `${urlString}&title=${this.titleSearchString}`;
                }

                if (this.isSearchByDescription) {
                    urlString = `${urlString}&description=${this.descriptionSearchString}`;
                }
                
                axios.get(`https://localhost:5001/books${urlString}`).then(response => {  
                    this.bookList = [...response.data.data];
                    this.hasNextPage = response.data.paginationMetadata.nextPageLink !== null;
                });
            },
            initVariables() {
                this.currentPage = 1;
                this.newPageNum = this.currentPage;
                this.hasNextPage = false;
                this.titleSearchString = "";
                this.descriptionSearchString = "";
                this.bookList = [];
                this.isSearchByTitle = false;
                this.isSearchByDescription = false;
            },
            navigateToPage() {
                this.currentPage = Number(this.newPageNum);
                this.getData();
            },
            navigateToNextPage() {
                if (this.hasNextPage === true) {
                    this.currentPage += 1;
                    this.newPageNum = this.currentPage;
                    this.getData();
                }
            },
            navigateToPreviousPage() {
                if (this.currentPage > 1) {
                    this.currentPage -= 1;
                    this.newPageNum = this.currentPage;
                    this.getData();
                }
            }
        },
        created() {
            this.initVariables();
            this.getData();
        } 
    };
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>

