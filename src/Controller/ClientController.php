<?php

namespace App\Controller;

use App\Entity\Client;
use App\Form\ClientType;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Response;
use Doctrine\ORM\EntityManagerInterface;
use Doctrine\Persistence\ManagerRegistry;
use Doctrine\Persistence\ObjectManager;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\Routing\Annotation\Route;
use Symfony\Component\Security\Http\Authenticator\Passport\Badge\UserBadge;
use Symfony\Component\HttpKernel\Exception\AccessDeniedHttpException;
use Symfony\Component\PasswordHasher\Hasher\UserPasswordHasherInterface;


class ClientController extends AbstractController
{
    private $entityManager ;
    public function _construct(EntityManagerInterface $entityManager){
        $this->entityManager = $entityManager;
    }
    // #[Route('/login', name: 'app_client')]
    // public function index(): Response
    // {
    //     return $this->render('login/index.html.twig', [
    //         'controller_name' => 'ClientController',
    //     ]);
    // }
   
    #[Route('/login', name: 'app_client')]

    public function login(Request  $request): Response
    {
        $clientLogin = new client();
       

        $form = $this->createForm(ClientType::class, $clientLogin);

        $form->handleRequest($request);

        if($form->isSubmitted() && $form->isValid() ){
            //$password = $clientLogin->getPassword();
            // $hashedPassword = $passwordHasher->hashPassword($clientLogin,$password);
            // $clientLogin->setPassword($password);

    
          // $this->entityManager->persist($clientLogin);
          // $this->entityManager->flush();
            // $em->persist($clientLogin);
            // $em->flush();
        }

        return $this->render('client/index.html.twig',['form' =>$form->createView()]);
    }


}
