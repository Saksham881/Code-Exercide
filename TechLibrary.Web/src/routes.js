import Vue from 'vue';
import VueRouter from 'vue-router';

Vue.use(VueRouter);

const Home = () => import(/* webpackChunkName: "Home" */ './components/Home.vue');
const Book = () => import(/* webpackChunkName: "Book" */ './components/Book.vue');
const EditBook = () => import(/* webpackChunkName: "EditBoot" */ './components/EditBook.vue');
const NewBook = () => import(/* webpackChunkName: "NewBook" */ './components/NewBook.vue');

const router = new VueRouter({
  routes: [
    { 
      name: 'home_view',
      path: '/',
      component: Home,
      props: false
    },
    {
      name: 'book_view',
      path: '/book/:id',
      component: Book,
      props: true
    },
    {
      name: 'edit_book',
      path: '/book/:id/edit',
      component: EditBook,
      props: true
    },
    {
      name: 'new_book',
      path: '/newBook',
      component: NewBook,
      props: false
    }
  ]
});

export default router;